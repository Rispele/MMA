using Rooms.Core.Dtos.Equipment;

namespace Rooms.Core.Dtos.Requests.EquipmentTypes;

public record PatchEquipmentTypeDto
{
    public required string Name { get; init; }
    public IEnumerable<EquipmentParameterDescriptorDto> Parameters { get; init; }
}