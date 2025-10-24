using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.Dtos.Requests.Equipments;

public record EquipmentsFilterDto
{
    public FilterMultiParameterDto<string>? RoomName { get; init; }
    public FilterMultiParameterDto<EquipmentTypeDto>? Types { get; init; }
    public FilterMultiParameterDto<EquipmentSchemaDto>? Schemas { get; init; }
    public FilterParameterDto<string>? InventoryNumber { get; init; }
    public FilterParameterDto<string>? SerialNumber { get; init; }
    public FilterParameterDto<string>? NetworkEquipmentIp { get; init; }
    public FilterParameterDto<string>? Comment { get; init; }
    public FilterMultiParameterDto<EquipmentStatus>? Statuses { get; init; }
}