using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentDtoConverter
{
    public static Equipment Convert(EquipmentDto equipment)
    {
        return new Equipment(
            equipment.Room.Map(RoomDtoConverter.Convert),
            equipment.SchemaDto.Map(EquipmentSchemaDtoConverter.Convert),
            equipment.InventoryNumber,
            equipment.SerialNumber,
            equipment.NetworkEquipmentIp,
            equipment.Comment,
            equipment.Status);
    }
}