using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.DtoConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class EquipmentTypeDtoMapper
{
    public static partial EquipmentTypeDto MapEquipmentTypeToDto(EquipmentType equipmentType);
}