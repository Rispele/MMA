using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentDtoConverter
{
    public static Equipment Convert(EquipmentDto equipment)
    {
        return new Equipment
        {
            RoomId = equipment.RoomId,
            SchemaId = equipment.SchemaId,
            InventoryNumber = equipment.InventoryNumber,
            SerialNumber = equipment.SerialNumber,
            NetworkEquipmentIp = equipment.NetworkEquipmentIp,
            Comment = equipment.Comment,
            Status = equipment.Status,
        };
    }
}