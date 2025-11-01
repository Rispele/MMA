using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentTypeDtoConverter
{
    public static EquipmentType Convert(EquipmentTypeDto type)
    {
        return new EquipmentType
        {
            Id = type.Id,
            Name = type.Name,
            Parameters = type.Parameters.Select(x => x.Map(Convert)).ToList(),
        };
    }

    public static EquipmentParameterDescriptor Convert(EquipmentParameterDescriptorDto dto)
    {
        return new EquipmentParameterDescriptor
        {
            Name = dto.Name,
            Required = dto.Required
        };
    }
}