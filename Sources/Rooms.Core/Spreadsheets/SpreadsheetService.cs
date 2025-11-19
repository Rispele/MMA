using Commons;
using Commons.Optional;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Services.Interfaces;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExportModels;
using Rooms.Core.Spreadsheets.Specifications;

namespace Rooms.Core.Spreadsheets;

public class SpreadsheetService(
    ISpreadsheetExporter exporter,
    IEquipmentTypeService equipmentTypeService,
    IEquipmentSchemaService equipmentSchemaService,
    IEquipmentService equipmentService,
    IRoomService roomService,
    IOperatorDepartmentService operatorDepartmentService)
{
    private const int ExportLimit = 10_000;

    public async Task<FileExportDto> ExportRoomRegistry(CancellationToken cancellationToken)
    {
        var request = new GetRoomsRequestDto
        {
            AfterRoomId = -1,
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
        
        return exporter.Export<RoomRegistrySpreadsheetSpecification, RoomRegistrySpreadsheetExportDto>(dataToExport, cancellationToken);
    }

    public async Task<FileExportDto> ExportEquipmentRegistry(CancellationToken cancellationToken)
    {
        var request = new GetEquipmentsDto(BatchNumber: 0, ExportLimit, AfterEquipmentId: -1, Filter: null);
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
            cancellationToken);
    }

    public async Task<FileExportDto> ExportEquipmentSchemaRegistry(CancellationToken cancellationToken)
    {
        var request = new GetEquipmentSchemasDto(BatchNumber: 0, ExportLimit, AfterEquipmentSchemaId: -1, Filter: null);
        var types = await equipmentSchemaService.FilterEquipmentSchemas(request, cancellationToken);
        var dataToExport = types.EquipmentSchemas
            .Select(schema => new EquipmentSchemaRegistrySpreadsheetExportDto
            {
                Name = schema.Name,
                TypeName = schema.Type.Name,
                Parameters = schema.ParameterValues.Select(t => $"{t.Key} = {t.Value}").JoinStrings(Environment.NewLine)
            })
            .ToArray();

        return exporter.Export<EquipmentSchemaRegistrySpreadsheetSpecification, EquipmentSchemaRegistrySpreadsheetExportDto>(
            dataToExport,
            cancellationToken);
    }

    public async Task<FileExportDto> ExportEquipmentTypeRegistry(CancellationToken cancellationToken)
    {
        var request = new GetEquipmentTypesDto(BatchNumber: 0, ExportLimit, AfterEquipmentTypeId: -1, Filter: null);
        var types = await equipmentTypeService.FilterEquipmentTypes(request, cancellationToken);
        var dataToExport = types.EquipmentTypes
            .Select(type => new EquipmentTypeRegistrySpreadsheetExportDto
            {
                Name = type.Name
            })
            .ToArray();

        return exporter.Export<EquipmentTypeRegistrySpreadsheetSpecification, EquipmentTypeRegistrySpreadsheetExportDto>(
            dataToExport,
            cancellationToken);
    }
}