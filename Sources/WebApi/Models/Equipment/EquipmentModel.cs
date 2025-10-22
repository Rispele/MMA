using Rooms.Domain.Models.Equipment;
using WebApi.Models.Room;

namespace WebApi.Models.Equipment;

public class EquipmentModel
{
    public int Id { get; init; }
    public RoomModel RoomModel { get; init; } = default!;
    public required EquipmentSchemaModel SchemaModel { get; init; }
    public string? InventoryNumber { get; init; }
    public string? SerialNumber { get; init; }
    public string? NetworkEquipmentIp { get; init; }
    public string? Comment { get; init; }
    public EquipmentStatus? Status { get; init; }
}