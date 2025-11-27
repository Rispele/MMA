namespace Rooms.Core.Interfaces.Dtos.Equipment;

public record EquipmentParameterDescriptorDto
{
    public required string Name { get; set; }
    public bool Required { get; set; }
}