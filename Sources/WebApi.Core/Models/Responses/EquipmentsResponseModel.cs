using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Responses;

public record EquipmentsResponseModel
{
    public EquipmentModel[] Equipments { get; init; } = [];
    public int Count { get; init; }
}