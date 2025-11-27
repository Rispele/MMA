namespace Rooms.Core.Services.Spreadsheets.ExportModels;

internal record EquipmentTypeRegistrySpreadsheetExportDto
{
    public required string Name { get; init; } = null!;
}