using WebApi.Models.Equipment;

namespace WebApi.Models.Responses;

public class EquipmentSchemasResponseModel
{
    public EquipmentSchemaModel[] EquipmentSchemas { get; init; } = [];
    public int Count { get; init; }
}