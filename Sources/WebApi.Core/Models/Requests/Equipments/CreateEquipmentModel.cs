using Rooms.Domain.Propagated.Equipments;

namespace WebApi.Core.Models.Requests.Equipments;

public record CreateEquipmentModel
{
    public int RoomId { get; init; }
    public int SchemaId { get; init; }
    public string? InventoryNumber { get; init; }
    public string? SerialNumber { get; init; }
    public string? NetworkEquipmentIp { get; init; }
    public string? Comment { get; init; }
    public EquipmentStatus? Status { get; init; }
    public int Count { get; init; }
}