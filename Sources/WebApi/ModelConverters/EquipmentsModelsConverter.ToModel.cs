using Commons;
using Rooms.Core.Dtos.Equipment;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;

namespace WebApi.ModelConverters;

public static partial class EquipmentsModelsConverter
{
    public static EquipmentModel Convert(EquipmentDto equipment)
    {
        return new EquipmentModel
        {
            Id = equipment.Id,
            RoomModel = equipment.Room.Map(RoomsModelsConverter.Convert),
            SchemaModel = equipment.SchemaDto.Map(Convert),
            InventoryNumber = equipment.InventoryNumber,
            SerialNumber = equipment.SerialNumber,
            NetworkEquipmentIp = equipment.NetworkEquipmentIp,
            Comment = equipment.Comment,
            Status = equipment.Status,
        };
    }

    public static PatchEquipmentModel ConvertToPatchModel(EquipmentDto dto)
    {
        return new PatchEquipmentModel
        {
            Room = dto.Room,
            Schema = dto.SchemaDto,
            InventoryNumber = dto.InventoryNumber,
            SerialNumber = dto.SerialNumber,
            NetworkEquipmentIp = dto.NetworkEquipmentIp,
            Comment = dto.Comment,
            Status = dto.Status,
        };
    }

    public static EquipmentSchemaModel Convert(EquipmentSchemaDto entity)
    {
        return new EquipmentSchemaModel
        {
            Id = entity.Id,
            TypeModel = entity.TypeDto.Map(Convert),
            ParameterValues = entity.ParameterValues,
        };
    }

    public static EquipmentTypeModel Convert(EquipmentTypeDto type)
    {
        return new EquipmentTypeModel
        {
            Id = type.Id,
            Name = type.Name,
            Parameters = type.Parameters.Map(Convert)
        };
    }

    public static EquipmentParameterDescriptorModel[] Convert(EquipmentParameterDescriptorDto[] descriptors)
    {
        return descriptors.Select(x => new EquipmentParameterDescriptorModel
        {
            Name = x.Name,
            Required = x.Required,
        }).ToArray();
    }
}