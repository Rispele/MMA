using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.InstituteCoordinators;

namespace Rooms.Core.Queries.Implementations.InstituteResponsible;

public sealed record FindInstituteResponsibleByIdQuery(int InstituteResponsibleId)
    : ISingleQuerySpecification<FindInstituteResponsibleByIdQuery, InstituteCoordinator>;