using WebApi.Models.Equipment;

namespace WebApi.Models.Responses;

public record EquipmentsResponse
{
    public EquipmentModel[] Equipments { get; init; } = [];
    public int Count { get; init; }
}