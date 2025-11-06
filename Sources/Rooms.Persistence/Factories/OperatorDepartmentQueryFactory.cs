using Rooms.Core.Dtos.Requests.OperatorDepartments;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Persistence.Queries.OperatorDepartments;

namespace Rooms.Persistence.Factories;

public class OperatorDepartmentQueryFactory : IOperatorDepartmentQueryFactory
{
    public IFilterOperatorDepartmentsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterOperatorDepartmentId = -1,
        OperatorDepartmentsFilterDto? filter = null)
    {
        return new FilterOperatorDepartmentsQuery
        {
            BatchSize = batchSize,
            BatchNumber = batchNumber,
            AfterOperatorDepartmentId = afterOperatorDepartmentId,
            Filter = filter
        };
    }

    public IFindOperatorDepartmentByIdQuery FindById(int operatorDepartmentId)
    {
        return new FindOperatorDepartmentsByIdQuery
        {
            OperatorDepartmentId = operatorDepartmentId
        };
    }
}