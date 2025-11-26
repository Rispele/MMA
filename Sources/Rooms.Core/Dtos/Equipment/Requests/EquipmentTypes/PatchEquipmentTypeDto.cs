namespace Rooms.Core.Dtos.Equipment.Requests.EquipmentTypes;

public record PatchEquipmentTypeDto
{
    public required string Name { get; init; }
    public IEnumerable<EquipmentParameterDescriptorDto> Parameters { get; init; }
}