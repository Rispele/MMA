namespace WebApi.Core.Models.Equipment;

public class EquipmentTypeModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required IEnumerable<EquipmentParameterDescriptorModel> Parameters { get; init; }
}