using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Services.Equipments.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentSchemaDtoMapper
{
    public static partial EquipmentSchemaDto MapEquipmentSchemaToDto(EquipmentSchema equipmentSchema);
}