using Rooms.Core.Dtos.Requests.InstituteResponsible;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.InstituteResponsible;

public interface IFilterInstituteResponsibleQuery : IQuerySpecification<Domain.Models.InstituteResponsible.InstituteResponsible>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterInstituteResponsibleId { get; init; }
    public InstituteResponsibleFilterDto? Filter { get; init; }
}