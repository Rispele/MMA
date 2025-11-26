using Rooms.Core.Dtos.OperatorDepartments.Requests;
using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Queries.Implementations.OperatorDepartments;

public sealed record FilterOperatorDepartmentsQuery(
    int BatchSize,
    int BatchNumber,
    int AfterOperatorDepartmentId,
    OperatorDepartmentsFilterDto? Filter)
    : IQuerySpecification<FilterOperatorDepartmentsQuery, OperatorDepartment>;