namespace WebApi.Models.Requests.EquipmentSchemas;

public record CreateEquipmentSchemaModel
{
    public string Name { get; init; } = null!;
    public int EquipmentTypeId { get; init; }
    public Dictionary<string, string> ParameterValues { get; init; } = null!;
}