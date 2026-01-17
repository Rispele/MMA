namespace Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;

public record GetEquipmentSchemasDto(int BatchNumber, int BatchSize, EquipmentSchemasFilterDto? Filter);