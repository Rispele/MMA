using WebApi.Models.Equipment;

namespace WebApi.Models.Responses;

public class EquipmentTypesResponseModel
{
    public EquipmentTypeModel[] EquipmentTypes { get; init; } = [];
    public int Count { get; init; }
}