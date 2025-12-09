using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using WebApi.Core.Models.Equipment;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.EquipmentSchemas;

// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.Core.ModelConverters;

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

    [MapProperty(nameof(GetRequest<EquipmentSchemasFilterModel>.PageSize), nameof(GetEquipmentSchemasDto.BatchSize))]
    [MapProperty(
        nameof(GetRequest<EquipmentSchemasFilterModel>.Page),
        nameof(GetEquipmentSchemasDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetEquipmentSchemasDto MapGetEquipmentSchemaFromModel(GetRequest<EquipmentSchemasFilterModel> model);
}