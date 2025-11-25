using Rooms.Core.Dtos.Equipment;

namespace Rooms.Core.Dtos.Requests.EquipmentTypes;

public record CreateEquipmentTypeDto
{
    public required string Name { get; init; }
    public EquipmentParameterDescriptorDto[] Parameters { get; init; }
}