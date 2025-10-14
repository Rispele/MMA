namespace Rooms.Domain.Models.Equipment;

public class EquipmentTypeDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public EquipmentParameterDescriptorDto[] Parameters { get; set; }
}