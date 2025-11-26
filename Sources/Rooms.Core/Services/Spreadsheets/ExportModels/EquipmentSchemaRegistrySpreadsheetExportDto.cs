namespace Rooms.Core.Services.Spreadsheets.ExportModels;

public record EquipmentSchemaRegistrySpreadsheetExportDto
{
    public required string Name { get; init; } = null!;
    public required string TypeName { get; init; } = null!;
    public required string Parameters { get; init; } = null!;
}