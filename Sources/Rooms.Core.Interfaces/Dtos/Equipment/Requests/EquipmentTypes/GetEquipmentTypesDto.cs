namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;

public record GetEquipmentTypesDto(int BatchNumber, int BatchSize, EquipmentTypesFilterDto? Filter);