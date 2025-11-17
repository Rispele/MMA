using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

public sealed record FilterEquipmentSchemasQuery(
    int BatchSize,
    int BatchNumber,
    int AfterEquipmentSchemaId,
    EquipmentSchemasFilterDto? Filter) : IQuerySpecification<FilterEquipmentSchemasQuery, EquipmentSchema>;