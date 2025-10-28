namespace Rooms.Domain.Models.Equipment;

public class EquipmentSchema
{
    public EquipmentSchema()
    {
    }

    public EquipmentSchema(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues,
        List<Equipment> equipments)
    {
        Name = name;
        EquipmentType = equipmentType;
        ParameterValues = parameterValues;
        Equipments = equipments;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int EquipmentTypeId { get; set; }
    public EquipmentType EquipmentType { get; set; } = null!;
    public Dictionary<string, string> ParameterValues { get; set; } = null!;
    public List<Equipment> Equipments { get; set; } = [];

    public static EquipmentSchema New(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues,
        List<Equipment> equipments)
    {
        return new EquipmentSchema(name, equipmentType, parameterValues, equipments);
    }

    public void Update(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues,
        List<Equipment> equipments)
    {
        Name = name;
        EquipmentType = equipmentType;
        ParameterValues = parameterValues;
        Equipments = equipments;
    }
}