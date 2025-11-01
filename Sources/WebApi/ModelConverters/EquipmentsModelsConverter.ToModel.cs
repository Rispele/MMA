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
            RoomId = equipment.RoomId,
            SchemaId = equipment.Schema.Id,
            Schema = equipment.Schema.Map(EquipmentSchemasModelsConverter.Convert),
            InventoryNumber = equipment.InventoryNumber,
            SerialNumber = equipment.SerialNumber,
            NetworkEquipmentIp = equipment.NetworkEquipmentIp,
            Comment = equipment.Comment,
            Status = equipment.Status
        };
    }

    public static PatchEquipmentModel ConvertToPatchModel(EquipmentDto dto)
    {
        return new PatchEquipmentModel
        {
            RoomId = dto.RoomId,
            Schema = dto.Schema,
            InventoryNumber = dto.InventoryNumber,
            SerialNumber = dto.SerialNumber,
            NetworkEquipmentIp = dto.NetworkEquipmentIp,
            Comment = dto.Comment,
            Status = dto.Status
        };
    }
}