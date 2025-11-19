namespace Rooms.Core.Spreadsheets.ExportModels;

public class EquipmentSchemaRegistrySpreadsheetExportDto
{
    public required string Name { get; set; } = null!;
    public required string TypeName { get; set; } = null!;
    public required string Parameters { get; set; } = null!;
}