namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;

public record PatchEquipmentSchemaDto
{
    public string Name { get; init; } = default!;
    public int EquipmentTypeId { get; init; }
    public Dictionary<string, string> ParameterValues { get; init; } = default!;
}