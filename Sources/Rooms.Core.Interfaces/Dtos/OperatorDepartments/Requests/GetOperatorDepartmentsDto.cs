namespace Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;

public record GetOperatorDepartmentsDto(int BatchNumber, int BatchSize, int AfterOperatorDepartmentId, OperatorDepartmentsFilterDto? Filter);