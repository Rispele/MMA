namespace WebApi.Core.Models.Equipment;

public class EquipmentSchemaModel
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public int TypeId { get; init; }
    public EquipmentTypeModel Type { get; init; } = null!;
    public Dictionary<string, string> ParameterValues { get; init; } = null!;
}