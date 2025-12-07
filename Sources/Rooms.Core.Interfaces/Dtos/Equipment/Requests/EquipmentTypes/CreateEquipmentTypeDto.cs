namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;

public record CreateEquipmentTypeDto
{
    public required string Name { get; init; }
    public required EquipmentParameterDescriptorDto[] Parameters { get; init; }
}