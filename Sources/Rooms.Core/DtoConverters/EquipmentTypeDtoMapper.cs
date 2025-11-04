using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.DtoConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentTypeDtoMapper
{
    [MapperIgnoreSource(nameof(EquipmentType.Schemas))]
    public static partial EquipmentTypeDto MapEquipmentTypeToDto(EquipmentType equipmentType);

    [MapperIgnoreTarget(nameof(EquipmentType.Schemas))]
    public static partial EquipmentType MapEquipmentTypeFromDto(EquipmentTypeDto equipmentType);
}