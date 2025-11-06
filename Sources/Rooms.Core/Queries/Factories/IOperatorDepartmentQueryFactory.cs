using Rooms.Core.Dtos.Requests.OperatorDepartments;
using Rooms.Core.Queries.Implementations.OperatorDepartments;

namespace Rooms.Core.Queries.Factories;

public interface IOperatorDepartmentQueryFactory
{
    public IFilterOperatorDepartmentsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterOperatorDepartmentId = -1,
        OperatorDepartmentsFilterDto? filter = null);

    public IFindOperatorDepartmentByIdQuery FindById(int operatorDepartmentId);
}