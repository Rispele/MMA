namespace Rooms.Domain.Models.EquipmentModels;

public class EquipmentType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public EquipmentParameterDescriptor[] Parameters { get; set; }
    public ICollection<EquipmentSchema> Schemas { get; set; }
}