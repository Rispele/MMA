namespace Rooms.Core.Dtos.Requests.Equipments;

public record GetEquipmentsDto(int BatchNumber, int BatchSize, int AfterEquipmentId, EquipmentsFilterDto? Filter);