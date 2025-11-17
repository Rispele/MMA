using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.InstituteResponsible;

public sealed record FindInstituteResponsibleByIdQuery(int InstituteResponsibleId)
    : ISingleQuerySpecification<FindInstituteResponsibleByIdQuery, Domain.Models.InstituteResponsible.InstituteResponsible>;