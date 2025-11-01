namespace Rooms.Domain.Models.Equipment;

public class Equipment
{
    public int Id { get; set; }
    public required int RoomId { get; set; }
    public Room.Room Room { get; set; } = null!;
    public required int SchemaId { get; set; }
    public EquipmentSchema Schema { get; set; } = null!;
    public string? InventoryNumber { get; set; }
    public string? SerialNumber { get; set; }
    public string? NetworkEquipmentIp { get; set; }
    public string? Comment { get; set; }
    public EquipmentStatus? Status { get; set; }

    public void Update(
        Room.Room room,
        EquipmentSchema schema,
        string? inventoryNumber,
        string? serialNumber,
        string? networkEquipmentIp,
        string? comment,
        EquipmentStatus? status)
    {
        RoomId = room.Id;
        Room = room;
        SchemaId = schema.Id;
        Schema = schema;
        InventoryNumber = inventoryNumber;
        SerialNumber = serialNumber;
        NetworkEquipmentIp = networkEquipmentIp;
        Comment = comment;
        Status = status;
    }
}