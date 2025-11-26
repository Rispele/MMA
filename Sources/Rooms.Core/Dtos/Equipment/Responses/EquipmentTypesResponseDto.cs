namespace Rooms.Core.Dtos.Equipment.Responses;

public record EquipmentTypesResponseDto(EquipmentTypeDto[] EquipmentTypes, int Count, int? LastEquipmentTypeId);