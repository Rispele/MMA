using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentSchemaDtoMapper
{
    [MapProperty(nameof(EquipmentSchema.EquipmentType), nameof(EquipmentSchemaDto.Type), Use = nameof(MapTypeToDto))]
    [MapProperty(nameof(EquipmentSchema.EquipmentTypeId), nameof(EquipmentSchemaDto.TypeId))]
    [MapperIgnoreSource(nameof(EquipmentSchema.Equipments))]
    public static partial EquipmentSchemaDto MapEquipmentSchemaToDto(EquipmentSchema equipmentSchema);

    [MapProperty(nameof(EquipmentSchemaDto.Type), nameof(EquipmentSchema.EquipmentType), Use = nameof(MapTypeFromDto))]
    [MapProperty(nameof(EquipmentSchemaDto.TypeId), nameof(EquipmentSchema.EquipmentTypeId))]
    [MapperIgnoreTarget(nameof(EquipmentSchema.Equipments))]
    public static partial EquipmentSchema MapEquipmentSchemaFromDto(EquipmentSchemaDto equipmentSchema);

    private static EquipmentTypeDto MapTypeToDto(EquipmentType type) => EquipmentTypeDtoMapper.MapEquipmentTypeToDto(type);
    private static EquipmentType MapTypeFromDto(EquipmentTypeDto type) => EquipmentTypeDtoMapper.MapEquipmentTypeFromDto(type);
}