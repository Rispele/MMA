namespace WebApi.Models.Requests.EquipmentSchemas;

public record CreateEquipmentSchemaModel
{
    public string Name { get; init; } = default!;
    public int EquipmentTypeId { get; init; }
    public Dictionary<string, string> ParameterValues { get; init; } = default!;
    public IEnumerable<int> EquipmentIds { get; init; }
}