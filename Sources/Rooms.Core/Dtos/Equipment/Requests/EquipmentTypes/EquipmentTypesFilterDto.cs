using Rooms.Core.Dtos.Filtering;

namespace Rooms.Core.Dtos.Equipment.Requests.EquipmentTypes;

public record EquipmentTypesFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
}