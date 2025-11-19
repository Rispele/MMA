using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.DtoMappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentSchemaDtoMapper
{
    public static partial EquipmentSchemaDto MapEquipmentSchemaToDto(EquipmentSchema equipmentSchema);
}