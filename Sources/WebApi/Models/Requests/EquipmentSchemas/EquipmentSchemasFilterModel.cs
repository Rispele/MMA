using WebApi.Models.Requests.Filtering;

namespace WebApi.Models.Requests.EquipmentSchemas;

public record EquipmentSchemasFilterModel
{
    public FilterParameterModel<string>? Name { get; init; }
    public FilterParameterModel<string>? EquipmentTypeName { get; init; }
    public FilterParameterModel<string>? EquipmentParameters { get; init; }
}