namespace Rooms.Domain.Models.Equipment;

public class EquipmentParameterDescriptor
{
    public int Order { get; set; }
    public string Name { get; set; } = default!;
    public bool Required { get; set; }
}