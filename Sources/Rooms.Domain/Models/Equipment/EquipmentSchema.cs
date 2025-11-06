namespace Rooms.Domain.Models.Equipment;

public class EquipmentSchema
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required EquipmentType Type { get; set; } = null!;
    public required Dictionary<string, string> ParameterValues { get; set; }

    public void Update(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues)
    {
        Name = name;
        Type = equipmentType;
        ParameterValues = parameterValues;
    }
}