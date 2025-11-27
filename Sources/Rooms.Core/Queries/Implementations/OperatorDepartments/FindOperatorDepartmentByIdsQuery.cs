using Commons.Domain.Queries.Abstractions;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Queries.Implementations.OperatorDepartments;

internal record FindOperatorDepartmentByIdsQuery(int[] OperatorDepartmentIds)
    : IQuerySpecification<FindOperatorDepartmentByIdsQuery, OperatorDepartment>;