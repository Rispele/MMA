namespace Rooms.Core.Dtos.Equipment;

public class EquipmentRegistryExcelExportDto
{
    public required string RoomName { get; set; } = null!;
    public required string EquipmentType { get; set; } = null!;
    public required string EquipmentSchemaName { get; set; } = null!;
    public string? InventoryNumber { get; set; }
    public string? SerialNumber { get; set; }
    public string? Comment { get; set; }
    public string? Status { get; set; }
}