using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

public sealed record FilterEquipmentTypesQuery(
    int BatchSize,
    int BatchNumber,
    int AfterEquipmentTypeId,
    EquipmentTypesFilterDto? Filter) : IQuerySpecification<FilterEquipmentTypesQuery, EquipmentType>;