using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using WebApi.Models.Equipment;
using WebApi.Models.Requests;
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

    [MapProperty(nameof(GetRequest<EquipmentsFilterModel>.PageSize), nameof(GetEquipmentsDto.BatchSize))]
    [MapProperty(
        nameof(GetRequest<EquipmentsFilterModel>.Page),
        nameof(GetEquipmentsDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetEquipmentsDto MapGetEquipmentFromModel(GetRequest<EquipmentsFilterModel> model);

    public static partial CreateEquipmentDto MapCreateEquipmentFromModel(CreateEquipmentModel equipment);

    public static partial PatchEquipmentDto MapPatchEquipmentFromModel(PatchEquipmentModel equipment);
}