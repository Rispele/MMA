using WebApi.Models.Equipment;

namespace WebApi.Models.Requests.EquipmentTypes;

public record CreateEquipmentTypeModel
{
    public required string Name { get; init; }
    public IEnumerable<EquipmentParameterDescriptorModel> Parameters { get; init; }
}