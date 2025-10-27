using WebApi.Models.Equipment;

namespace WebApi.Models.Requests.EquipmentTypes;

public record CreateEquipmentTypeModel
{
    public required string Name { get; init; }
    public EquipmentParameterDescriptorModel[] Parameters { get; init; }
}