using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.Specifications;

namespace Rooms.Core.Spreadsheets;

public class SpreadsheetService(
    ISpreadsheetExporter exporter)
{
    public async Task<FileExportDto> ExportEquipmentRegistry(CancellationToken cancellationToken)
    {
        var exportDtos = new[]
        {
            new EquipmentRegistryExcelExportDto
            {
                RoomName = string.Empty,
                EquipmentType = string.Empty,
                EquipmentSchemaName = string.Empty,
            }
        };

        return exporter.Export<EquipmentRegistrySpreadsheetSpecification, EquipmentRegistryExcelExportDto>(
            exportDtos, 
            cancellationToken);
    }

    public async Task<FileExportDto> ExportEquipmentSchemaRegistry(CancellationToken cancellationToken)
    {
        var exportDtos = new[]
        {
            new EquipmentSchemaRegistryExcelExportDto
            {
                EquipmentName = string.Empty,
                EquipmentType = string.Empty,
                Parameters = string.Empty,
            }
        };

        return exporter.Export<EquipmentSchemaRegistrySpreadsheetWriterSpecification, EquipmentSchemaRegistryExcelExportDto>(
            exportDtos,
            cancellationToken);
    }


    public async Task<FileExportDto> ExportEquipmentTypeRegistry(CancellationToken cancellationToken)
    {
        var exportDtos = new[]
        {
            new EquipmentTypeRegistryExcelExportDto
            {
                Name = string.Empty,
            }
        };

        return exporter.Export<EquipmentTypeRegistrySpreadsheetWriterSpecification, EquipmentTypeRegistryExcelExportDto>(
            exportDtos,
            cancellationToken);
    }
}