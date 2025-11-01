namespace Rooms.Domain.Models.Equipment;

public class EquipmentType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<EquipmentParameterDescriptor> Parameters { get; set; } = [];
    public List<EquipmentSchema> Schemas { get; set; } = [];

    public void Update(
        string name,
        List<EquipmentParameterDescriptor> parameters,
        List<EquipmentSchema> schemas)
    {
        Name = name;
        Parameters = parameters;
        Schemas = schemas;
    }
}