using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Responses;

public class EquipmentTypesResponseModel
{
    public EquipmentTypeModel[] EquipmentTypes { get; init; } = [];
    public int Count { get; init; }
}