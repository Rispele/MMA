using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.EquipmentModels;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentDtoConverter
{
    public static EquipmentDto Convert(Equipment equipment)
    {
        return new EquipmentDto
        {
            Id = equipment.Id,
            Room = equipment.Room.Map(Core.DtoConverters.RoomDtoConverter.Convert),
            SchemaDto = equipment.Schema.Map(Convert),
            InventoryNumber = equipment.InventoryNumber,
            SerialNumber = equipment.SerialNumber,
            NetworkEquipmentIp = equipment.NetworkEquipmentIp,
            Comment = equipment.Comment,
            Status = equipment.Status,
        };
    }

    public static EquipmentSchemaDto Convert(EquipmentSchema entity)
    {
        return new EquipmentSchemaDto
        {
            Id = entity.Id,
            TypeDto = entity.Type.Map(Convert),
            ParameterValues = entity.ParameterValues,
        };
    }

    public static EquipmentTypeDto Convert(EquipmentType type)
    {
        return new EquipmentTypeDto
        {
            Id = type.Id,
            Name = type.Name,
            Parameters = type.Parameters.Map(Convert)
        };
    }

    public static EquipmentParameterDescriptorDto[] Convert(EquipmentParameterDescriptor[] descriptors)
    {
        return descriptors.Select(x => new EquipmentParameterDescriptorDto
        {
            Name = x.Name,
            Required = x.Required,
        }).ToArray();
    }
}