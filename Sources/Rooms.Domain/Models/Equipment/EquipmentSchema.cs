using JetBrains.Annotations;

namespace Rooms.Domain.Models.Equipment;

public class EquipmentSchema
{
    [UsedImplicitly]
    protected EquipmentSchema()
    {
    }

    public EquipmentSchema(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues,
        IEnumerable<Equipment> equipments)
    {
        Name = name;
        EquipmentType = equipmentType;
        ParameterValues = parameterValues;
        Equipments = equipments;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public EquipmentType EquipmentType { get; set; } = default!;
    public Dictionary<string, string> ParameterValues { get; set; } = default!;
    public IEnumerable<Equipment> Equipments { get; set; }

    public static EquipmentSchema New(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues,
        IEnumerable<Equipment> equipments)
    {
        return new EquipmentSchema(name, equipmentType, parameterValues, equipments);
    }

    public void Update(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues,
        IEnumerable<Equipment> equipments)
    {
        Name = name;
        EquipmentType = equipmentType;
        ParameterValues = parameterValues;
        Equipments = equipments;
    }
}