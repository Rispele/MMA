namespace Rooms.Core.Dtos.Equipment.Requests.EquipmentTypes;

public record GetEquipmentTypesDto(int BatchNumber, int BatchSize, int AfterEquipmentTypeId, EquipmentTypesFilterDto? Filter);