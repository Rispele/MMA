using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentSchemaDtoConverter
{
    public static EquipmentSchema Convert(EquipmentSchemaDto entity)
    {
        return new EquipmentSchema(
            entity.Name,
            entity.EquipmentType.Map(EquipmentTypeDtoConverter.Convert),
            entity.ParameterValues,
            entity.Equipments.Select(x => x.Map(EquipmentDtoConverter.Convert)));
    }
}