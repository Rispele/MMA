using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.OperatorDepartments;

public class FindOperatorDepartmentByIdsQueryHandler : IQueryHandler<FindOperatorDepartmentByIdsQuery, OperatorDepartment>
{
    public Task<IAsyncEnumerable<OperatorDepartment>> Handle(
        EntityQuery<FindOperatorDepartmentByIdsQuery, IAsyncEnumerable<OperatorDepartment>> request,
        CancellationToken cancellationToken)
    {
        var ids = request.Query.OperatorDepartmentIds;

        var response = request.Context.OperatorDepartments
            .Where(predicate: t => ids.Contains(t.Id))
            .AsAsyncEnumerable();

        return Task.FromResult(response);
    }
}