using Commons;
using Commons.Optional;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Interfaces.Dtos.Files;
using Rooms.Core.Interfaces.Dtos.Room.Requests;
using Rooms.Core.Interfaces.Services.Equipments;
using Rooms.Core.Interfaces.Services.OperatorDepartments;
using Rooms.Core.Interfaces.Services.Rooms;
using Rooms.Core.Interfaces.Services.Spreadsheets;
using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Core.Services.Spreadsheets.ExportModels;
using Rooms.Core.Services.Spreadsheets.Specifications;

namespace Rooms.Core.Services.Spreadsheets;

internal class SpreadsheetService(
    ISpreadsheetExporter exporter,
    IEquipmentTypeService equipmentTypeService,
    IEquipmentSchemaService equipmentSchemaService,
    IEquipmentService equipmentService,
    IRoomService roomService,
    IOperatorDepartmentService operatorDepartmentService) : ISpreadsheetService
{
    public const int ExportLimit = 10_000;

    public async Task<FileExportDto> ExportRoomRegistry(Stream outputStream, CancellationToken cancellationToken)
    {
        var request = new GetRoomsRequestDto
        {
            BatchNumber = 0,
            BatchSize = ExportLimit,
            Filter = null
        };

        var rooms = await roomService.FilterRooms(request, cancellationToken);

        var departmentsToFetch = rooms.Rooms.Select(t => t.OperatorDepartmentId).NotNull().ToArray();
        var operatorDepartments = await operatorDepartmentService.GetOperatorDepartmentsById(departmentsToFetch, cancellationToken);

        var operatorDepartmentById = operatorDepartments.ToDictionary(t => t.Id);

        var dataToExport = rooms.Rooms
            .Select(room => new RoomRegistrySpreadsheetExportDto
            {
                Room = room,
                OperatorDepartment = room.OperatorDepartmentId.AsOptional().Map(t => operatorDepartmentById[t]),
            })
            .ToArray();

        return exporter.Export<RoomRegistrySpreadsheetSpecification, RoomRegistrySpreadsheetExportDto>(dataToExport, outputStream, cancellationToken);
    }

    public async Task<FileExportDto> ExportEquipmentRegistry(Stream outputStream, CancellationToken cancellationToken)
    {
        var request = new GetEquipmentsDto(BatchNumber: 0, ExportLimit, Filter: null);
        var equipments = await equipmentService.FilterEquipments(request, cancellationToken);

        var roomsToFetch = equipments.Equipments.Select(t => t.RoomId).Distinct().ToArray();
        var rooms = await roomService.FindRoomByIds(roomsToFetch, cancellationToken);

        var roomsById = rooms.ToDictionary(t => t.Id);

        var dataToExport = equipments.Equipments
            .Select(equipment => new EquipmentRegistrySpreadsheetExportDto
            {
                RoomName = roomsById[equipment.RoomId].Name,
                TypeName = equipment.Schema.Type.Name,
                SchemaName = equipment.Schema.Name,
                Comment = equipment.Comment,
                InventoryNumber = equipment.InventoryNumber,
                SerialNumber = equipment.SerialNumber,
                Status = equipment.Status.ToString()
            })
            .ToArray();

        return exporter.Export<EquipmentRegistrySpreadsheetSpecification, EquipmentRegistrySpreadsheetExportDto>(
            dataToExport,
            outputStream,
            cancellationToken);
    }

    public async Task<FileExportDto> ExportEquipmentSchemaRegistry(Stream outputStream, CancellationToken cancellationToken)
    {
        var request = new GetEquipmentSchemasDto(BatchNumber: 0, ExportLimit, Filter: null);
        var types = await equipmentSchemaService.FilterEquipmentSchemas(request, cancellationToken);
        var dataToExport = types.EquipmentSchemas
            .Select(schema => new EquipmentSchemaRegistrySpreadsheetExportDto
            {
                Name = schema.Name,
                TypeName = schema.Type.Name,
                Parameters = schema.ParameterValues.Select(pair => $"{pair.Key} = {pair.Value}").JoinStrings(Environment.NewLine)
            })
            .ToArray();

        return exporter.Export<EquipmentSchemaRegistrySpreadsheetSpecification, EquipmentSchemaRegistrySpreadsheetExportDto>(
            dataToExport,
            outputStream,
            cancellationToken);
    }

    public async Task<FileExportDto> ExportEquipmentTypeRegistry(Stream outputStream, CancellationToken cancellationToken)
    {
        var request = new GetEquipmentTypesDto(BatchNumber: 0, ExportLimit, Filter: null);
        var types = await equipmentTypeService.FilterEquipmentTypes(request, cancellationToken);
        var dataToExport = types.EquipmentTypes
            .Select(type => new EquipmentTypeRegistrySpreadsheetExportDto
            {
                Name = type.Name
            })
            .ToArray();

        return exporter.Export<EquipmentTypeRegistrySpreadsheetSpecification, EquipmentTypeRegistrySpreadsheetExportDto>(
            dataToExport,
            outputStream,
            cancellationToken);
    }
}