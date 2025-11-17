using Rooms.Core.Dtos.Requests.InstituteResponsible;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.InstituteResponsible;

public sealed record FilterInstituteResponsibleQuery(
    int BatchSize,
    int BatchNumber,
    int AfterInstituteResponsibleId,
    InstituteResponsibleFilterDto? Filter) : IQuerySpecification<FilterInstituteResponsibleQuery, Domain.Models.InstituteResponsible.InstituteResponsible>;