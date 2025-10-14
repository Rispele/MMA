namespace Rooms.Domain.Models.Equipment;

public class EquipmentParameterDescriptorDto
{
    public int Order { get; set; }
    public string Name { get; set; } = default!;
    public bool Required { get; set; }
}