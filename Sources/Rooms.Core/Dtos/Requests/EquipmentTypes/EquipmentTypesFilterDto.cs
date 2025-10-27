using Rooms.Core.Dtos.Requests.Filtering;

namespace Rooms.Core.Dtos.Requests.EquipmentTypes;

public record EquipmentTypesFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
}