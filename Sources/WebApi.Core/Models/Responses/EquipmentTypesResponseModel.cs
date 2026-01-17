using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Responses;

public class EquipmentTypesResponseModel(EquipmentTypeModel[] EquipmentTypes, int TotalCount);