namespace Rooms.Core.Dtos.Requests.EquipmentSchemas;

public record GetEquipmentSchemasDto(int BatchNumber, int BatchSize, int AfterEquipmentSchemaId, EquipmentSchemasFilterDto? Filter);