using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.OperatorDepartments;

internal class FindOperatorDepartmentByIdsQueryHandler : IQueryHandler<RoomsDbContext, FindOperatorDepartmentByIdsQuery, OperatorDepartment>
{
    public Task<IAsyncEnumerable<OperatorDepartment>> Handle(
        EntityQuery<RoomsDbContext, FindOperatorDepartmentByIdsQuery, IAsyncEnumerable<OperatorDepartment>> request,
        CancellationToken cancellationToken)
    {
        var ids = request.Query.OperatorDepartmentIds;

        var response = request.Context.OperatorDepartments
            .Where(predicate: t => ids.Contains(t.Id))
            .AsAsyncEnumerable();

        return Task.FromResult(response);
    }
}