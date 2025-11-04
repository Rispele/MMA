using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentDtoMapper
{
    [MapProperty(nameof(Equipment.Schema), nameof(EquipmentDto.Schema), Use = nameof(MapSchemaToDto))]
    [MapperIgnoreSource(nameof(Equipment.Room))]
    public static partial EquipmentDto MapEquipmentToDto(Equipment equipment);

    [MapProperty(nameof(EquipmentDto.Schema.TypeId), nameof(Equipment.Schema.EquipmentTypeId))]
    [MapperIgnoreTarget(nameof(Equipment.Room))]
    public static partial Equipment MapEquipmentFromDto(EquipmentDto equipment);

    private static EquipmentSchemaDto MapSchemaToDto(EquipmentSchema schema) => EquipmentSchemaDtoMapper.MapEquipmentSchemaToDto(schema);

    private static EquipmentSchemaDto MapSchemaFromDto(EquipmentSchema schema) => EquipmentSchemaDtoMapper.MapEquipmentSchemaToDto(schema);
}