using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Services.Interfaces;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.Specifications;

namespace Rooms.Core.Spreadsheets;

public class SpreadsheetService(
    ISpreadsheetExporter exporter,
    IEquipmentTypeService equipmentTypeService,
    IEquipmentSchemaService equipmentSchemaService,
    IEquipmentService equipmentService,
    IRoomService roomService)
{
    private const int ExportLimit = 10_000;

    public async Task<FileExportDto> ExportEquipmentRegistry(CancellationToken cancellationToken)
    {
        var request = new GetEquipmentsDto(BatchNumber: 0, ExportLimit, AfterEquipmentId: -1, Filter: null);
        var equipments = await equipmentService.FilterEquipments(request, cancellationToken);
        var rooms = await roomService.FindRoomByIds(equipments.Equipments.Select(t => t.RoomId).Distinct().ToArray(), cancellationToken);

        var roomsById = rooms.ToDictionary(t => t.Id);

        var dataToExport = equipments.Equipments
            .Select(equipment => new EquipmentRegistryExcelExportDto
            {
                RoomName = roomsById.GetValueOrDefault(equipment.RoomId)?.Name ?? string.Empty,
                TypeName = equipment.Schema.Type.Name,
                SchemaName = equipment.Schema.Name,
                Comment = equipment.Comment,
                InventoryNumber = equipment.InventoryNumber,
                SerialNumber = equipment.SerialNumber,
                Status = equipment.Status.ToString()
            })
            .ToArray();

        return exporter.Export<EquipmentRegistrySpreadsheetSpecification, EquipmentRegistryExcelExportDto>(
            dataToExport,
            cancellationToken);
    }

    public async Task<FileExportDto> ExportEquipmentSchemaRegistry(CancellationToken cancellationToken)
    {
        var request = new GetEquipmentSchemasDto(BatchNumber: 0, ExportLimit, AfterEquipmentSchemaId: -1, Filter: null);
        var types = await equipmentSchemaService.FilterEquipmentSchemas(request, cancellationToken);
        var dataToExport = types.EquipmentSchemas
            .Select(schema => new EquipmentSchemaRegistryExcelExportDto
            {
                Name = schema.Name,
                TypeName = schema.Type.Name,
                Parameters = schema.ParameterValues.Select(t => $"{t.Key} = {t.Value}").JoinStrings(Environment.NewLine)
            })
            .ToArray();

        return exporter.Export<EquipmentSchemaRegistrySpreadsheetWriterSpecification, EquipmentSchemaRegistryExcelExportDto>(
            dataToExport,
            cancellationToken);
    }


    public async Task<FileExportDto> ExportEquipmentTypeRegistry(CancellationToken cancellationToken)
    {
        var request = new GetEquipmentTypesDto(BatchNumber: 0, ExportLimit, AfterEquipmentTypeId: -1, Filter: null);
        var types = await equipmentTypeService.FilterEquipmentTypes(request, cancellationToken);
        var dataToExport = types.EquipmentTypes
            .Select(type => new EquipmentTypeRegistryExcelExportDto
            {
                Name = type.Name
            })
            .ToArray();

        return exporter.Export<EquipmentTypeRegistrySpreadsheetWriterSpecification, EquipmentTypeRegistryExcelExportDto>(
            dataToExport,
            cancellationToken);
    }
}