using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentModelMapper
{
    public static partial EquipmentModel MapEquipmentToModel(EquipmentDto equipment);

    public static partial PatchEquipmentModel MapEquipmentToPatchModel(EquipmentDto equipment);

    public static partial CreateEquipmentDto MapCreateEquipmentFromModel(CreateEquipmentModel equipment);

    public static partial PatchEquipmentDto MapPatchEquipmentTypeFromModel(PatchEquipmentModel equipment);
}