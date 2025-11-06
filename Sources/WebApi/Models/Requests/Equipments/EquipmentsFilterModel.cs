using Rooms.Domain.Models.Equipments;
using WebApi.Models.Requests.Filtering;

namespace WebApi.Models.Requests.Equipments;

public record EquipmentsFilterModel
{
    public FilterMultiParameterModel<int>? Rooms { get; init; }
    public FilterMultiParameterModel<int>? Types { get; init; }
    public FilterMultiParameterModel<int>? Schemas { get; init; }
    public FilterParameterModel<string>? InventoryNumber { get; init; }
    public FilterParameterModel<string>? SerialNumber { get; init; }
    public FilterParameterModel<string>? NetworkEquipmentIp { get; init; }
    public FilterParameterModel<string>? Comment { get; init; }
    public FilterMultiParameterModel<EquipmentStatus>? Statuses { get; init; }
}