using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Queries.Implementations.OperatorDepartments;

public sealed record FindOperatorDepartmentByIdQuery(int OperatorDepartmentId) : ISingleQuerySpecification<FindOperatorDepartmentByIdQuery, OperatorDepartment>;