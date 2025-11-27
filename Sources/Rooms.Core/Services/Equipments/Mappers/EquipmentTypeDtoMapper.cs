using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Services.Equipments.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class EquipmentTypeDtoMapper
{
    public static partial EquipmentTypeDto MapEquipmentTypeToDto(EquipmentType equipmentType);
}