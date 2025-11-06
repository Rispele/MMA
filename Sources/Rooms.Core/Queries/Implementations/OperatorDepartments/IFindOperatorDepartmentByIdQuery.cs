using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Queries.Implementations.OperatorDepartments;

public interface IFindOperatorDepartmentByIdQuery : ISingleQuerySpecification<OperatorDepartment>
{
    public int OperatorDepartmentId { get; init; }
}