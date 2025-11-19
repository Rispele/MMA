namespace Rooms.Core.Dtos.Equipment;

public class EquipmentSchemaRegistryExcelExportDto
{
    public required string Name { get; set; } = null!;
    public required string TypeName { get; set; } = null!;
    public required string Parameters { get; set; } = null!;
}