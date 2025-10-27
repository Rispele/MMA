namespace Rooms.Core.Dtos.Equipment;

public class EquipmentTypeDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public IEnumerable<EquipmentParameterDescriptorDto> Parameters { get; set; }
}