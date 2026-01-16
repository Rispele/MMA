using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Responses;

public record EquipmentsResponseModel(EquipmentModel[] Equipments, int TotalCount);