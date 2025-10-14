using Rooms.Core.Implementations.Dtos.Room;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.Implementations.Dtos.Equipment;

public class EquipmentDto
{
    public int Id { get; set; }
    public RoomDto? Room { get; set; } = default!;
    public required EquipmentTypeDto TypeDto { get; set; }
    public required EquipmentSchemaDto SchemaDto { get; set; }
    public string? InventoryNumber { get; set; } = default!;
    public string? SerialNumber { get; set; } = default!;
    public string? NetworkEquipmentIp { get; set; } = default!;
    public string? Comment { get; set; }
    public EquipmentStatusDto? Status { get; set; } = default!;
}