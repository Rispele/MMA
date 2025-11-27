using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Booking.Domain.Models.InstituteCoordinators;
using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.InstituteResponsible;

internal sealed record FilterInstituteResponsibleQuery(
    int BatchSize,
    int BatchNumber,
    int AfterInstituteResponsibleId,
    InstituteCoordinatorFilterDto? Filter)
    : IQuerySpecification<FilterInstituteResponsibleQuery, InstituteCoordinator>;