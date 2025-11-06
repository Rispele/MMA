using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentSchemas;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentSchemaModelMapper
{
    public static partial EquipmentSchemaModel MapEquipmentSchemaToModel(EquipmentSchemaDto equipmentSchema);

    [MapProperty(nameof(EquipmentSchemaDto.Type.Id), nameof(PatchEquipmentSchemaModel.EquipmentTypeId))]
    public static partial PatchEquipmentSchemaModel MapEquipmentSchemaToPatchModel(EquipmentSchemaDto equipmentSchema);

    public static partial CreateEquipmentSchemaDto MapCreateEquipmentSchemaFromModel(CreateEquipmentSchemaModel equipmentSchema);

    public static partial PatchEquipmentSchemaDto MapPatchEquipmentTypeFromModel(PatchEquipmentSchemaModel equipmentSchema);

    public static partial EquipmentSchemaDto MapEquipmentSchemaFromModel(EquipmentSchemaModel equipmentSchema);
}