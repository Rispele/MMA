using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentSchemaDtoConverter
{
    public static EquipmentSchemaDto Convert(EquipmentSchema entity)
    {
        return new EquipmentSchemaDto
        {
            Id = entity.Id,
            Name = entity.Name,
            EquipmentType = entity.EquipmentType.Map(EquipmentTypeDtoConverter.Convert),
            ParameterValues = entity.ParameterValues,
            Equipments = entity.Equipments.Select(x => x.Map(EquipmentDtoConverter.Convert))
        };
    }
}