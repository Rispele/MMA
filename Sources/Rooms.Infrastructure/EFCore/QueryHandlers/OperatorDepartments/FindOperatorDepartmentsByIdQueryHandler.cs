using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.OperatorDepartments;

public class FindOperatorDepartmentsByIdQueryHandler : ISingleQueryHandler<RoomsDbContext, FindOperatorDepartmentByIdQuery, OperatorDepartment?>
{
    public Task<OperatorDepartment?> Handle(
        EntityQuery<RoomsDbContext, FindOperatorDepartmentByIdQuery, OperatorDepartment?> request,
        CancellationToken cancellationToken)
    {
        var id = request.Query.OperatorDepartmentId;

        return request.Context.OperatorDepartments.FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}