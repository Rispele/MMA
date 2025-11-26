using Rooms.Core.Dtos.InstituteResponsible.Requests;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.InstituteResponsible;

public sealed record FilterInstituteResponsibleQuery(
    int BatchSize,
    int BatchNumber,
    int AfterInstituteResponsibleId,
    InstituteResponsibleFilterDto? Filter)
    : IQuerySpecification<FilterInstituteResponsibleQuery, Domain.Models.InstituteResponsibles.InstituteResponsible>;