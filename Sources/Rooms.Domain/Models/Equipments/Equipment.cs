namespace Rooms.Domain.Models.Equipments;

public class Equipment
{
    public int Id { get; set; }
    public required int RoomId { get; set; }
    public required EquipmentSchema Schema { get; set; } = null!;
    public string? InventoryNumber { get; set; }
    public string? SerialNumber { get; set; }
    public string? NetworkEquipmentIp { get; set; }
    public string? Comment { get; set; }
    public EquipmentStatus? Status { get; set; }

    public void Update(
        int roomId,
        EquipmentSchema schema,
        string? inventoryNumber,
        string? serialNumber,
        string? networkEquipmentIp,
        string? comment,
        EquipmentStatus? status)
    {
        RoomId = roomId;
        Schema = schema;
        InventoryNumber = inventoryNumber;
        SerialNumber = serialNumber;
        NetworkEquipmentIp = networkEquipmentIp;
        Comment = comment;
        Status = status;
    }
}