using Rooms.Core.Dtos.Room;
using Rooms.Domain.Models.EquipmentModels;

namespace Rooms.Core.Dtos.Equipment;

public class EquipmentDto
{
    public int Id { get; set; }
    public RoomDto Room { get; set; } = default!;
    public required EquipmentSchemaDto SchemaDto { get; set; }
    public string? InventoryNumber { get; set; } = default!;
    public string? SerialNumber { get; set; } = default!;
    public string? NetworkEquipmentIp { get; set; } = default!;
    public string? Comment { get; set; }
    public EquipmentStatus? Status { get; set; } = default!;
}