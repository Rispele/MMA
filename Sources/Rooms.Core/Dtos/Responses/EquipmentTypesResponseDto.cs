using Rooms.Core.Dtos.Equipment;

namespace Rooms.Core.Dtos.Responses;

public record EquipmentTypesResponseDto(EquipmentTypeDto[] EquipmentTypes, int Count, int? LastEquipmentTypeId);