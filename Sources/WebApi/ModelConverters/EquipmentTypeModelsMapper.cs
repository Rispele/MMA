using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentTypes;
// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentTypeModelsMapper
{
    public static partial EquipmentTypeModel MapEquipmentTypeToModel(EquipmentTypeDto equipmentType);

    [MapperIgnoreSource(nameof(EquipmentSchemaDto.Id))]
    public static partial PatchEquipmentTypeModel MapEquipmentTypeToPatchModel(EquipmentTypeDto equipmentType);

    public static partial CreateEquipmentTypeDto MapCreateEquipmentTypeFromModel(CreateEquipmentTypeModel equipmentType);

    public static partial PatchEquipmentTypeDto MapPatchEquipmentTypeFromModel(PatchEquipmentTypeModel equipmentType);

    [MapProperty(nameof(GetEquipmentTypesModel.AfterEquipmentTypeId), nameof(GetEquipmentTypesDto.AfterEquipmentTypeId))]
    [MapProperty(nameof(GetEquipmentTypesModel.PageSize), nameof(GetEquipmentTypesDto.BatchSize))]
    [MapProperty(
        nameof(GetEquipmentTypesModel.Page),
        nameof(GetEquipmentTypesDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetEquipmentTypesDto MapGetEquipmentTypesFromModel(GetEquipmentTypesModel model);
}