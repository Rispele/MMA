using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

public static partial class EquipmentDtoConverter
{
    public static EquipmentDto Convert(Equipment equipment)
    {
        return new EquipmentDto
        {
            Id = equipment.Id,
            RoomId = equipment.RoomId,
            SchemaId = equipment.SchemaId,
            Schema = equipment.Schema.Map(EquipmentSchemaDtoConverter.Convert),
            InventoryNumber = equipment.InventoryNumber,
            SerialNumber = equipment.SerialNumber,
            NetworkEquipmentIp = equipment.NetworkEquipmentIp,
            Comment = equipment.Comment,
            Status = equipment.Status
        };
    }
}