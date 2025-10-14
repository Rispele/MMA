using Rooms.Core.Implementations.Dtos.Equipment;

namespace Rooms.Core.Implementations.Dtos.Responses;

public record EquipmentsBatchDto(EquipmentDto[] Equipments, int Count, int? LastEquipmentId);