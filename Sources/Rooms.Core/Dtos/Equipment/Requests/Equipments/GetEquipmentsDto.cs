namespace Rooms.Core.Dtos.Equipment.Requests.Equipments;

public record GetEquipmentsDto(int BatchNumber, int BatchSize, int AfterEquipmentId, EquipmentsFilterDto? Filter);