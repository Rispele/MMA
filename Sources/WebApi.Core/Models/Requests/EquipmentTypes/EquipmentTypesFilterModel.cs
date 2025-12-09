using WebApi.Core.Models.Requests.Filtering;

namespace WebApi.Core.Models.Requests.EquipmentTypes;

public record EquipmentTypesFilterModel
{
    public FilterParameterModel<string>? Name { get; init; }
}