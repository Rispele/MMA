using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.OperatorDepartments;

public class FindOperatorDepartmentsByIdQueryHandler : ISingleQueryHandler<FindOperatorDepartmentByIdQuery, OperatorDepartment?>
{
    public Task<OperatorDepartment?> Handle(
        EntityQuery<FindOperatorDepartmentByIdQuery, OperatorDepartment?> request,
        CancellationToken cancellationToken)
    {
        var id = request.Query.OperatorDepartmentId;

        return request.Context.OperatorDepartments.FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}