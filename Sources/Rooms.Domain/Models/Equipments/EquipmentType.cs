namespace Rooms.Domain.Models.Equipments;

public class EquipmentType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<EquipmentParameterDescriptor> Parameters { get; set; } = [];

    public void Update(
        string name,
        List<EquipmentParameterDescriptor> parameters)
    {
        Name = name;
        Parameters = parameters;
    }
}