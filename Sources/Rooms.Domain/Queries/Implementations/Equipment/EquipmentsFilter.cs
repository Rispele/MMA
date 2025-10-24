using Rooms.Domain.Models.EquipmentModels;
using Rooms.Domain.Queries.Implementations.Filtering;

namespace Rooms.Domain.Queries.Implementations.Equipments;

public record EquipmentsFilter
{
    public FilterMultiParameter<string>? RoomName { get; init; }
    public FilterMultiParameter<EquipmentType>? Types { get; init; }
    public FilterMultiParameter<EquipmentSchema>? Schemas { get; init; }
    public FilterParameter<string>? InventoryNumber { get; init; }
    public FilterParameter<string>? SerialNumber { get; init; }
    public FilterParameter<string>? NetworkEquipmentIp { get; init; }
    public FilterParameter<string>? Comment { get; init; }
    public FilterMultiParameter<EquipmentStatus>? Statuses { get; init; }
}