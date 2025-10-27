namespace Rooms.Core.Dtos.Requests.EquipmentSchemas;

public record CreateEquipmentSchemaDto
{
    public string Name { get; init; } = default!;
    public int EquipmentTypeId { get; init; }
    public Dictionary<string, string> ParameterValues { get; init; } = default!;
}