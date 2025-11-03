namespace Rooms.Core.Dtos.Equipment;

public class EquipmentSchemaRegistryExcelExportDto
{
    public required string EquipmentName { get; set; } = null!;
    public required string EquipmentType { get; set; } = null!;
    public required string Parameters { get; set; } = null!;
}