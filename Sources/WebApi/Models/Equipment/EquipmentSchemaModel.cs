namespace WebApi.Models.Equipment;

public class EquipmentSchemaModel
{
    public int Id { get; init; }
    public string Name { get; set; } = default!;
    public int EquipmentTypeId { get; init; }
    public Dictionary<string, string> ParameterValues { get; init; } = default!;
    public IEnumerable<int> EquipmentIds { get; init; }
}