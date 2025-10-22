using Rooms.Core.Dtos.Equipment;

namespace Rooms.Core.Dtos.Responses;

public record EquipmentsResponseDto(EquipmentDto[] Equipments, int Count, int? LastEquipmentId);