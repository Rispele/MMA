using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentModelMapper
{
    [MapProperty(nameof(EquipmentDto.Schema), nameof(EquipmentModel.Schema), Use = nameof(MapSchema))]
    public static partial EquipmentModel MapEquipmentToModel(EquipmentDto equipment);

    [MapProperty(nameof(EquipmentDto.Schema), nameof(PatchEquipmentModel.Schema), Use = nameof(MapSchema))]
    [MapperIgnoreSource(nameof(EquipmentDto.Id))]
    [MapperIgnoreSource(nameof(EquipmentDto.SchemaId))]
    public static partial PatchEquipmentModel MapEquipmentToPatchModel(EquipmentDto equipment);

    public static partial CreateEquipmentDto MapCreateEquipmentFromModel(CreateEquipmentModel equipment);

    public static partial PatchEquipmentDto MapPatchEquipmentTypeFromModel(PatchEquipmentModel equipment);

    [MapProperty(nameof(EquipmentModel.Schema.Type), nameof(EquipmentDto.Schema.Type))]
    public static partial EquipmentDto MapEquipmentFromModel(EquipmentModel equipment);

    private static EquipmentSchemaModel MapSchema(EquipmentSchemaDto schema)
    {
        return EquipmentSchemaModelMapper.MapEquipmentSchemaToModel(schema);
    }
}