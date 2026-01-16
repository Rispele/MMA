using Commons.Domain.Queries.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

internal sealed record FilterEquipmentsQuery(
    int BatchSize,
    int BatchNumber,
    EquipmentsFilterDto? Filter) : IPaginatedQuerySpecification<FilterEquipmentsQuery, Domain.Models.Equipments.Equipment>;