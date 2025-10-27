namespace Rooms.Core.Dtos.Requests.EquipmentTypes;

public record GetEquipmentTypesDto(int BatchNumber, int BatchSize, int AfterEquipmentTypeId, EquipmentTypesFilterDto? Filter);