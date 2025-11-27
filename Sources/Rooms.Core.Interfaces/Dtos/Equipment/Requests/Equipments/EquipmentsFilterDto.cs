using Commons.Core.Models.Filtering;
using Rooms.Domain.Propagated.Equipments;

namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;

public record EquipmentsFilterDto
{
    public FilterMultiParameterDto<int>? Rooms { get; init; }
    public FilterMultiParameterDto<int>? Types { get; init; }
    public FilterMultiParameterDto<int>? Schemas { get; init; }
    public FilterParameterDto<string>? InventoryNumber { get; init; }
    public FilterParameterDto<string>? SerialNumber { get; init; }
    // public FilterParameterDto<string>? NetworkEquipmentIp { get; init; }
    // public FilterParameterDto<string>? Comment { get; init; }
    public FilterMultiParameterDto<EquipmentStatus>? Statuses { get; init; }
}