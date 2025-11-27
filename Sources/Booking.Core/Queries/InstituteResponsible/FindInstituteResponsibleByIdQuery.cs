using Booking.Domain.Models.InstituteCoordinators;
using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.InstituteResponsible;

public sealed record FindInstituteResponsibleByIdQuery(int InstituteResponsibleId)
    : ISingleQuerySpecification<FindInstituteResponsibleByIdQuery, InstituteCoordinator>;