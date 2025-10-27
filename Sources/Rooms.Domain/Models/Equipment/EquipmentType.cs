using JetBrains.Annotations;

namespace Rooms.Domain.Models.Equipment;

public class EquipmentType
{
    [UsedImplicitly]
    protected EquipmentType()
    {
    }

    public EquipmentType(
        string name,
        IEnumerable<EquipmentParameterDescriptor> parameters,
        IEnumerable<EquipmentSchema> schemas)
    {
        Name = name;
        Parameters = parameters;
        Schemas = schemas;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<EquipmentParameterDescriptor> Parameters { get; set; }
    public IEnumerable<EquipmentSchema> Schemas { get; set; }

    public static EquipmentType New(
        string name,
        IEnumerable<EquipmentParameterDescriptor> parameters,
        IEnumerable<EquipmentSchema> schemas)
    {
        return new EquipmentType(name, parameters, schemas);
    }

    public void Update(
        string name,
        IEnumerable<EquipmentParameterDescriptor> parameters,
        IEnumerable<EquipmentSchema> schemas)
    {
        Name = name;
        Parameters = parameters;
        Schemas = schemas;
    }
}