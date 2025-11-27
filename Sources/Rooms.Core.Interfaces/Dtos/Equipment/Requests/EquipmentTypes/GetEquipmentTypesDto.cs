namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;

public record GetEquipmentTypesDto(int BatchNumber, int BatchSize, int AfterEquipmentTypeId, EquipmentTypesFilterDto? Filter);