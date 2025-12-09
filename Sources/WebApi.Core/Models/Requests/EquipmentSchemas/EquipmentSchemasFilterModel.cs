using WebApi.Core.Models.Requests.Filtering;

namespace WebApi.Core.Models.Requests.EquipmentSchemas;

public record EquipmentSchemasFilterModel
{
    public FilterParameterModel<string>? Name { get; init; }
    public FilterParameterModel<string>? EquipmentTypeName { get; init; }
    public FilterParameterModel<string>? EquipmentParameters { get; init; }
}