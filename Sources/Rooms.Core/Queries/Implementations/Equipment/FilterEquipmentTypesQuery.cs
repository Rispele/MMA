using Commons.Domain.Queries.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

internal sealed record FilterEquipmentTypesQuery(
    int BatchSize,
    int BatchNumber,
    int AfterEquipmentTypeId,
    EquipmentTypesFilterDto? Filter) : IQuerySpecification<FilterEquipmentTypesQuery, EquipmentType>;