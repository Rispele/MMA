using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Booking.Domain.Models.InstituteCoordinators;
using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.InstituteCoordinators;

internal sealed record FilterInstituteCoordinatorsQuery(
    int BatchSize,
    int BatchNumber,
    int AfterInstituteResponsibleId,
    InstituteCoordinatorFilterDto? Filter)
    : IQuerySpecification<FilterInstituteCoordinatorsQuery, InstituteCoordinator>;