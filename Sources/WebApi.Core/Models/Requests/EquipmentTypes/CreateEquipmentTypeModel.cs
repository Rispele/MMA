using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Requests.EquipmentTypes;

public record CreateEquipmentTypeModel
{
    public required string Name { get; init; }
    public required IEnumerable<EquipmentParameterDescriptorModel> Parameters { get; init; }
}