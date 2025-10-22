using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Persistence;

namespace Rooms.Domain.Models.Equipment;

[EntityTypeConfiguration<EquipmentEntityTypeConfiguration, Equipment>]
public class Equipment
{
    public int Id { get; set; }
    public Room.Room Room { get; set; } = default!;
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
        Room.Room room,
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
        Room.Room room,
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
        Room.Room room,
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