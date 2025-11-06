namespace Rooms.Core.Dtos.Requests.OperatorDepartments;

public record GetOperatorDepartmentsDto(int BatchNumber, int BatchSize, int AfterOperatorDepartmentId, OperatorDepartmentsFilterDto? Filter);