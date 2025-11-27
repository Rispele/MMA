using Rooms.Core.Dtos.InstituteCoordinator.Requests;
using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.InstituteCoordinators;

namespace Rooms.Core.Queries.Implementations.InstituteResponsible;

public sealed record FilterInstituteResponsibleQuery(
    int BatchSize,
    int BatchNumber,
    int AfterInstituteResponsibleId,
    InstituteCoordinatorFilterDto? Filter)
    : IQuerySpecification<FilterInstituteResponsibleQuery, InstituteCoordinator>;