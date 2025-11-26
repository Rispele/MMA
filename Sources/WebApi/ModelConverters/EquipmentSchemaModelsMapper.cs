using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Equipment.Requests.EquipmentSchemas;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentSchemas;
// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentSchemaModelsMapper
{
    [MapProperty(nameof(EquipmentSchemaDto.Type), nameof(EquipmentSchemaModel.Type), Use = nameof(@EquipmentTypeModelsMapper.MapEquipmentTypeToModel))]
    public static partial EquipmentSchemaModel MapEquipmentSchemaToModel(EquipmentSchemaDto equipmentSchema);

    [MapProperty(nameof(EquipmentSchemaDto.Type.Id), nameof(PatchEquipmentSchemaModel.EquipmentTypeId))]
    [MapperIgnoreSource(nameof(EquipmentSchemaDto.Id))]
    public static partial PatchEquipmentSchemaModel MapEquipmentSchemaToPatchModel(EquipmentSchemaDto equipmentSchema);

    public static partial CreateEquipmentSchemaDto MapCreateEquipmentSchemaFromModel(CreateEquipmentSchemaModel equipmentSchema);

    public static partial PatchEquipmentSchemaDto MapPatchEquipmentSchemaFromModel(PatchEquipmentSchemaModel equipmentSchema);

    [MapProperty(nameof(GetEquipmentSchemasModel.AfterEquipmentSchemaId), nameof(GetEquipmentSchemasDto.AfterEquipmentSchemaId))]
    [MapProperty(nameof(GetEquipmentSchemasModel.PageSize), nameof(GetEquipmentSchemasDto.BatchSize))]
    [MapProperty(
        nameof(GetEquipmentSchemasModel.Page),
        nameof(GetEquipmentSchemasDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetEquipmentSchemasDto MapGetEquipmentSchemaFromModel(GetEquipmentSchemasModel model);
}