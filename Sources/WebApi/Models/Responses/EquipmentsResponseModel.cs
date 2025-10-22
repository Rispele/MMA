using WebApi.Models.Equipment;

namespace WebApi.Models.Responses;

public record EquipmentsResponseModel
{
    public EquipmentModel[] Equipments { get; init; } = [];
    public int Count { get; init; }
}