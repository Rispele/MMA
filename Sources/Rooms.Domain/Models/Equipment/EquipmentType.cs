namespace Rooms.Domain.Models.Equipment;

public class EquipmentType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public EquipmentParameterDescriptor[] Parameters { get; set; }
}