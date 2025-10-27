namespace Rooms.Core.Dtos.Equipment;

public class EquipmentSchemaDto
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public EquipmentTypeDto EquipmentType { get; init; }
    public Dictionary<string, string> ParameterValues { get; init; } = default!;
    public IEnumerable<EquipmentDto> Equipments { get; init; }
}