using Rooms.Core.Dtos.Requests.Filtering;

namespace Rooms.Core.Dtos.Requests.EquipmentSchemas;

public record EquipmentSchemasFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
    public FilterParameterDto<string>? EquipmentTypeName { get; init; }
    public FilterParameterDto<string>? EquipmentParameters { get; init; }
}