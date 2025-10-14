using Rooms.Core.Implementations.Dtos.Room;

namespace WebApi.Models.Equipment;

public class EquipmentModel
{
    public int Id { get; init; }
    public RoomDto? Room { get; init; }
    public required EquipmentTypeModel TypeModel { get; init; }
    public required EquipmentSchemaModel SchemaModel { get; init; }
    public string? InventoryNumber { get; init; }
    public string? SerialNumber { get; init; }
    public string? NetworkEquipmentIp { get; init; }
    public string? Comment { get; init; }
    public EquipmentStatusModel? Status { get; init; }
}