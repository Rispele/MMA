using JetBrains.Annotations;
using Rooms.Domain.Models.RoomModels;

namespace Rooms.Domain.Models.EquipmentModels;

public class Equipment
{
    public int Id { get; set; }
    public Room Room { get; set; } = default!;
    public EquipmentSchema Schema { get; set; }
    public string? InventoryNumber { get; set; }
    public string? SerialNumber { get; set; }
    public string? NetworkEquipmentIp { get; set; }
    public string? Comment { get; set; }
    public EquipmentStatus? Status { get; set; }

    [UsedImplicitly]
    protected Equipment()
    {
    }

    public Equipment(
        Room room,
        EquipmentSchema schema,
        string? inventoryNumber,
        string? serialNumber,
        string? networkEquipmentIp,
        string? comment,
        EquipmentStatus? status)
    {
        Room = room;
        Schema = schema;
        InventoryNumber = inventoryNumber;
        SerialNumber = serialNumber;
        NetworkEquipmentIp = networkEquipmentIp;
        Comment = comment;
        Status = status;
    }

    public static Equipment New(
        Room room,
        EquipmentSchema schema,
        string? inventoryNumber,
        string? serialNumber,
        string? networkEquipmentIp,
        string? comment,
        EquipmentStatus? status)
    {
        return new Equipment(room, schema, inventoryNumber, serialNumber, networkEquipmentIp, comment, status);
    }

    public void Update(
        Room room,
        EquipmentSchema schema,
        string? inventoryNumber,
        string? serialNumber,
        string? networkEquipmentIp,
        string? comment,
        EquipmentStatus? status)
    {
        Room = room;
        Schema = schema;
        InventoryNumber = inventoryNumber;
        SerialNumber = serialNumber;
        NetworkEquipmentIp = networkEquipmentIp;
        Comment = comment;
        Status = status;
    }
}