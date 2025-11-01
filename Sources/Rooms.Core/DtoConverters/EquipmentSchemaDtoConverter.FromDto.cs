using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentSchemaDtoConverter
{
    public static EquipmentSchema Convert(EquipmentSchemaDto entity)
    {
        return new EquipmentSchema
        {
            Id = entity.Id,
            Name = entity.Name,
            EquipmentTypeId = entity.TypeId,
            EquipmentType = entity.Type.Map(EquipmentTypeDtoConverter.Convert),
            ParameterValues = entity.ParameterValues,
        };
    }
}