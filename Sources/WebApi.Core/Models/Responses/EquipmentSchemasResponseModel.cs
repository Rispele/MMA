using WebApi.Core.Models.Equipment;

namespace WebApi.Core.Models.Responses;

public class EquipmentSchemasResponseModel
{
    public EquipmentSchemaModel[] EquipmentSchemas { get; init; } = [];
    public int Count { get; init; }
}