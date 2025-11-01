namespace Rooms.Domain.Models.Equipment;

public class EquipmentSchema
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int EquipmentTypeId { get; set; }
    public EquipmentType EquipmentType { get; set; } = null!;
    public required Dictionary<string, string> ParameterValues { get; set; }
    public List<Equipment> Equipments { get; set; } = [];

    public void Update(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues,
        List<Equipment> equipments)
    {
        Name = name;
        EquipmentTypeId = equipmentType.Id;
        EquipmentType = equipmentType;
        ParameterValues = parameterValues;
        Equipments = equipments;
    }
}