using Booking.Domain.Models.InstituteCoordinators;
using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.InstituteCoordinators;

internal sealed record FindInstituteCoordinatorByIdQuery(int InstituteResponsibleId)
    : ISingleQuerySpecification<FindInstituteCoordinatorByIdQuery, InstituteCoordinator>;