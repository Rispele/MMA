namespace Rooms.Domain.Models.Equipment;

public class EquipmentType
{
    public EquipmentType()
    {
    }

    public EquipmentType(
        string name,
        List<EquipmentParameterDescriptor> parameters,
        List<EquipmentSchema> schemas)
    {
        Name = name;
        Parameters = parameters;
        Schemas = schemas;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<EquipmentParameterDescriptor> Parameters { get; set; } = [];
    public List<EquipmentSchema> Schemas { get; set; } = [];

    public static EquipmentType New(
        string name,
        List<EquipmentParameterDescriptor> parameters,
        List<EquipmentSchema> schemas)
    {
        return new EquipmentType(name, parameters, schemas);
    }

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