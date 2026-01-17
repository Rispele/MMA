using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Requests.EquipmentTypes;

public record PatchEquipmentTypeModel
{
    public required string Name { get; init; }
    public required IEnumerable<EquipmentParameterDescriptorModel> Parameters { get; init; }
}