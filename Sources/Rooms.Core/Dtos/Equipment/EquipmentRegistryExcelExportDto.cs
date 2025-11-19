namespace Rooms.Core.Dtos.Equipment;

public class EquipmentRegistryExcelExportDto
{
    public required string RoomName { get; set; } = null!;
    public required string TypeName { get; set; } = null!;
    public required string SchemaName { get; set; } = null!;
    public string? InventoryNumber { get; set; }
    public string? SerialNumber { get; set; }
    public string? Comment { get; set; }
    public string? Status { get; set; }
}