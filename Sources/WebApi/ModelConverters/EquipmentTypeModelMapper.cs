using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentTypes;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentTypeModelMapper
{
    public static partial EquipmentTypeModel MapEquipmentTypeToModel(EquipmentTypeDto equipmentType);

    [MapperIgnoreSource(nameof(EquipmentSchemaDto.Id))]
    public static partial PatchEquipmentTypeModel MapEquipmentTypeToPatchModel(EquipmentTypeDto equipmentType);

    public static partial CreateEquipmentTypeDto MapCreateEquipmentTypeFromModel(CreateEquipmentTypeModel equipmentType);

    public static partial PatchEquipmentTypeDto MapPatchEquipmentTypeFromModel(PatchEquipmentTypeModel equipmentType);

    public static partial EquipmentTypeDto MapEquipmentTypeFromModel(EquipmentTypeModel equipmentType);
}