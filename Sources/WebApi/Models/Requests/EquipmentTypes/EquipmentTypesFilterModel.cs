using WebApi.Models.Requests.Filtering;

namespace WebApi.Models.Requests.EquipmentTypes;

public record EquipmentTypesFilterModel
{
    public FilterParameterModel<string>? Name { get; init; }
}