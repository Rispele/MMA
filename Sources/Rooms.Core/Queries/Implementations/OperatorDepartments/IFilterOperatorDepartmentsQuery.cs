using Rooms.Core.Dtos.Requests.OperatorDepartments;
using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Queries.Implementations.OperatorDepartments;

public interface IFilterOperatorDepartmentsQuery : IQuerySpecification<OperatorDepartment>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterOperatorDepartmentId { get; init; }
    public OperatorDepartmentsFilterDto? Filter { get; init; }
}