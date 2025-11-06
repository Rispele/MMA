using Rooms.Domain.Models.Equipments;

namespace WebApi.Models.Equipment;

public class EquipmentModel
{
    public int Id { get; init; }
    public int RoomId { get; init; }
    public int SchemaId { get; init; }
    public EquipmentSchemaModel Schema { get; init; } = null!;
    public string? InventoryNumber { get; init; }
    public string? SerialNumber { get; init; }
    public string? NetworkEquipmentIp { get; init; }
    public string? Comment { get; init; }
    public EquipmentStatus? Status { get; init; }
}