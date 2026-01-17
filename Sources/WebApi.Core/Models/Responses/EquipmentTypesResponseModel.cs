using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Responses;

public record EquipmentTypesResponseModel(EquipmentTypeModel[] EquipmentTypes, int TotalCount);