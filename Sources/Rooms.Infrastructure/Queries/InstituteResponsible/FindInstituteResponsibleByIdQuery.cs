using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.InstituteResponsible;
using Rooms.Infrastructure.Queries.Abstractions;

namespace Rooms.Infrastructure.Queries.InstituteResponsible;

public readonly struct FindInstituteResponsibleByIdQuery :
    IFindInstituteResponsibleByIdQuery,
    ISingleQueryImplementer<Domain.Models.InstituteResponsible.InstituteResponsible?, RoomsDbContext>
{
    public required int InstituteResponsibleId { get; init; }

    public Task<Domain.Models.InstituteResponsible.InstituteResponsible?> Apply(
        RoomsDbContext source,
        CancellationToken cancellationToken)
    {
        var id = InstituteResponsibleId;

        return source.InstituteResponsible.FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}