namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;

public record GetEquipmentsDto(int BatchNumber, int BatchSize, EquipmentsFilterDto? Filter);