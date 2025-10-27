namespace WebApi.Models.Equipment;

public class EquipmentTypeModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public IEnumerable<EquipmentParameterDescriptorModel> Parameters { get; init; }
}