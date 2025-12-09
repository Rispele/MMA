namespace WebApi.Core.Models.Requests.EquipmentSchemas;

public record PatchEquipmentSchemaModel
{
    public string Name { get; init; } = default!;
    public int EquipmentTypeId { get; init; }
    public Dictionary<string, string> ParameterValues { get; init; } = default!;
}