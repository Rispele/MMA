namespace Rooms.Core.Dtos.Equipment.Responses;

public record EquipmentsResponseDto(EquipmentDto[] Equipments, int Count, int? LastEquipmentId);