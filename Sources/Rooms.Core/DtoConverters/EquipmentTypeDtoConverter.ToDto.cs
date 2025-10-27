using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentTypeDtoConverter
{
    public static EquipmentTypeDto Convert(EquipmentType type)
    {
        return new EquipmentTypeDto
        {
            Id = type.Id,
            Name = type.Name,
            Parameters = type.Parameters.Select(x => x.Map(Convert))
        };
    }

    public static EquipmentParameterDescriptorDto Convert(EquipmentParameterDescriptor descriptor)
    {
        return new EquipmentParameterDescriptorDto
        {
            Name = descriptor.Name,
            Required = descriptor.Required
        };
    }
}