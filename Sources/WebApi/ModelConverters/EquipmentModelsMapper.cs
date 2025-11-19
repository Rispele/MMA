using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;
// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentModelsMapper
{
    [MapProperty(nameof(EquipmentDto.Schema), nameof(EquipmentModel.Schema), Use = nameof(@EquipmentSchemaModelsMapper.MapEquipmentSchemaToModel))]
    public static partial EquipmentModel MapEquipmentToModel(EquipmentDto equipment);

    [MapperIgnoreSource(nameof(EquipmentDto.Id))]
    public static partial PatchEquipmentModel MapEquipmentToPatchModel(EquipmentDto equipment);

    [MapProperty(nameof(GetEquipmentsModel.AfterEquipmentId), nameof(GetEquipmentsDto.AfterEquipmentId))]
    [MapProperty(nameof(GetEquipmentsModel.PageSize), nameof(GetEquipmentsDto.BatchSize))]
    [MapProperty(
        nameof(GetEquipmentsModel.Page),
        nameof(GetEquipmentsDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetEquipmentsDto MapGetEquipmentFromModel(GetEquipmentsModel model);

    public static partial CreateEquipmentDto MapCreateEquipmentFromModel(CreateEquipmentModel equipment);

    public static partial PatchEquipmentDto MapPatchEquipmentFromModel(PatchEquipmentModel equipment);
}