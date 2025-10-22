using Rooms.Core.Implementations.Dtos.Requests.Filtering;
using Rooms.Domain.Models.Equipment;
using WebApi.Models.Equipment;

namespace WebApi.Models.Requests;

public record EquipmentsFilter
{
    public FilterMultiParameterDto<string>? RoomName { get; init; }
    public FilterMultiParameterDto<EquipmentTypeModel>? Types { get; init; }
    public FilterMultiParameterDto<EquipmentSchemaModel>? Schemas { get; init; }
    public FilterParameterDto<string>? InventoryNumber { get; init; }
    public FilterParameterDto<string>? SerialNumber { get; init; }
    public FilterParameterDto<string>? NetworkEquipmentIp { get; init; }
    public FilterParameterDto<string>? Comment { get; init; }
    public FilterMultiParameterDto<EquipmentStatus>? Statuses { get; init; }
}