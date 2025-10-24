using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.EquipmentModels;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentDtoConverter
{
    public static Equipment Convert(EquipmentDto equipment)
    {
        return new Equipment(
            equipment.Room.Map(Core.DtoConverters.RoomDtoConverter.Convert),
            equipment.SchemaDto.Map(Convert),
            equipment.InventoryNumber,
            equipment.SerialNumber,
            equipment.NetworkEquipmentIp,
            equipment.Comment,
            equipment.Status);
    }

    public static EquipmentSchema Convert(EquipmentSchemaDto entity)
    {
        return new EquipmentSchema
        {
            Id = entity.Id,
            Type = entity.TypeDto.Map(Convert),
            ParameterValues = entity.ParameterValues,
        };
    }

    public static EquipmentType Convert(EquipmentTypeDto type)
    {
        return new EquipmentType
        {
            Id = type.Id,
            Name = type.Name,
            Parameters = type.Parameters.Map(Convert)
        };
    }

    public static EquipmentParameterDescriptor[] Convert(EquipmentParameterDescriptorDto[] descriptors)
    {
        return descriptors.Select(x => new EquipmentParameterDescriptor
        {
            Name = x.Name,
            Required = x.Required,
        }).ToArray();
    }
}