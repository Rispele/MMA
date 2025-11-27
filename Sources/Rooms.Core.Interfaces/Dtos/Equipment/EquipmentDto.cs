using Rooms.Domain.Propagated.Equipments;

namespace Rooms.Core.Interfaces.Dtos.Equipment;

public class EquipmentDto
{
    public int Id { get; init; }
    public int RoomId { get; init; }
    public required EquipmentSchemaDto Schema { get; init; }
    public string? InventoryNumber { get; init; }
    public string? SerialNumber { get; init; }
    public string? NetworkEquipmentIp { get; init; }
    public string? Comment { get; init; }
    public EquipmentStatus? Status { get; init; }
}