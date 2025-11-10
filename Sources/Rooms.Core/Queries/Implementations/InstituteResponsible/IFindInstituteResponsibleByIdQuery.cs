using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.InstituteResponsible;

public interface IFindInstituteResponsibleByIdQuery : ISingleQuerySpecification<Domain.Models.InstituteResponsible.InstituteResponsible>
{
    public int InstituteResponsibleId { get; init; }
}