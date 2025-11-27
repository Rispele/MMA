using Commons.Domain.Queries.Abstractions;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Queries.Implementations.OperatorDepartments;

internal sealed record FilterOperatorDepartmentsQuery(
    int BatchSize,
    int BatchNumber,
    int AfterOperatorDepartmentId,
    OperatorDepartmentsFilterDto? Filter)
    : IQuerySpecification<FilterOperatorDepartmentsQuery, OperatorDepartment>;