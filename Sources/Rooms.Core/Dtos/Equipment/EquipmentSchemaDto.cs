namespace Rooms.Core.Dtos.Equipment;

public class EquipmentSchemaDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public EquipmentTypeDto Type { get; init; } = null!;
    public Dictionary<string, string> ParameterValues { get; init; } = null!;
}