using Rooms.Core.Dtos.Equipment;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentSchemas;

namespace WebApi.ModelConverters;

public static partial class EquipmentSchemasModelsConverter
{
    public static EquipmentSchemaModel Convert(EquipmentSchemaDto entity)
    {
        return new EquipmentSchemaModel
        {
            Id = entity.Id,
            Name = entity.Name,
            EquipmentTypeId = entity.EquipmentType.Id,
            ParameterValues = entity.ParameterValues
        };
    }

    public static PatchEquipmentSchemaModel ConvertToPatchModel(EquipmentSchemaDto entity)
    {
        return new PatchEquipmentSchemaModel
        {
            Name = entity.Name,
            EquipmentTypeId = entity.EquipmentType.Id,
            ParameterValues = entity.ParameterValues,
            EquipmentIds = entity.Equipments.Select(x => x.Id)
        };
    }
}