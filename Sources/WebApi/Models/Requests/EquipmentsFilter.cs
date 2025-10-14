using Rooms.Core.Implementations.Dtos.Requests.Filtering;
using Rooms.Domain.Models.Equipment;

namespace WebApi.Models.Requests;

public record EquipmentsFilter
{
    public FilterParameterDto<string>? RoomName { get; init; }
    public FilterMultiParameterDto<EquipmentTypeDto>? Types { get; init; }
    public FilterMultiParameterDto<EquipmentSchemaDto>? Schema { get; init; }
    public FilterParameterDto<string>? InventoryNumber { get; init; }
    public FilterParameterDto<string>? SerialNumber { get; init; }
    public FilterParameterDto<string>? NetworkEquipmentIp { get; init; }
    public FilterParameterDto<string>? Comment { get; init; }
    public FilterMultiParameterDto<EquipmentStatusDto>? Status { get; init; }
}