using Commons.Domain.Queries.Abstractions;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Queries.Implementations.OperatorDepartments;

internal sealed record FindOperatorDepartmentByIdQuery(int OperatorDepartmentId)
    : ISingleQuerySpecification<FindOperatorDepartmentByIdQuery, OperatorDepartment>;
