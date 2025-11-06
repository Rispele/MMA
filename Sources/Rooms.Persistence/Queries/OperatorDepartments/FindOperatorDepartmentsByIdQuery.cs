using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.OperatorDepartments;

public readonly struct FindOperatorDepartmentsByIdQuery :
    IFindOperatorDepartmentByIdQuery,
    ISingleQueryImplementer<OperatorDepartment?, RoomsDbContext>
{
    public required int OperatorDepartmentId { get; init; }

    public Task<OperatorDepartment?> Apply(
        RoomsDbContext source,
        CancellationToken cancellationToken)
    {
        var id = OperatorDepartmentId;

        return source.OperatorDepartments.FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}