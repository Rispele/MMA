namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;

public record CreateEquipmentSchemaDto
{
    public string Name { get; init; } = null!;
    public int EquipmentTypeId { get; init; }
    public Dictionary<string, string> ParameterValues { get; init; } = null!;
}