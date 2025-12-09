using Rooms.Domain.Propagated.Equipments;
using WebApi.Core.Models.Requests.Filtering;

namespace WebApi.Core.Models.Requests.Equipments;

public record EquipmentsFilterModel
{
    public FilterMultiParameterModel<int>? Rooms { get; init; }
    public FilterMultiParameterModel<int>? Types { get; init; }
    public FilterMultiParameterModel<int>? Schemas { get; init; }
    public FilterParameterModel<string>? InventoryNumber { get; init; }
    public FilterParameterModel<string>? SerialNumber { get; init; }
    // public FilterParameterModel<string>? NetworkEquipmentIp { get; init; }
    // public FilterParameterModel<string>? Comment { get; init; }
    public FilterMultiParameterModel<EquipmentStatus>? Statuses { get; init; }
}