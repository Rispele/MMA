using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Persistence;

namespace Rooms.Domain.Models.Equipment;

[EntityTypeConfiguration<EquipmentEntityTypeConfiguration, Equipment>]
public class Equipment
{
    public int Id { get; set; }
    public Room.Room? Room { get; set; }
    public required EquipmentType Type { get; set; }
    public required EquipmentSchema Schema { get; set; }
    public string? InventoryNumber { get; set; }
    public string? SerialNumber { get; set; }
    public string? NetworkEquipmentIp { get; set; }
    public string? Comment { get; set; }
    public EquipmentStatus? Status { get; set; }

    [UsedImplicitly]
    protected Equipment()
    {
    }
}