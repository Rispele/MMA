namespace Rooms.Core.Services.Spreadsheets.ExportModels;

public record EquipmentTypeRegistrySpreadsheetExportDto
{
    public required string Name { get; init; } = null!;
}