using Commons.Core.Models.Filtering;

namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;

public record EquipmentTypesFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
}