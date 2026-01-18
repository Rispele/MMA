using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Responses;

public record EquipmentSchemasResponseModel(EquipmentSchemaModel[] EquipmentSchemas, int TotalCount);