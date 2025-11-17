using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public sealed record FilterEquipmentsQuery(
    int BatchSize,
    int BatchNumber,
    int AfterEquipmentId,
    EquipmentsFilterDto? Filter) : IQuerySpecification<FilterEquipmentsQuery, Domain.Models.Equipments.Equipment>;