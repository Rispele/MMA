using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Services.Equipments.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class EquipmentDtoMapper
{
    public static partial EquipmentDto MapEquipmentToDto(Equipment equipment);
}