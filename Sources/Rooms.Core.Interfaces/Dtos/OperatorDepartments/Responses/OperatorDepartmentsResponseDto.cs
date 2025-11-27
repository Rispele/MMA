namespace Rooms.Core.Interfaces.Dtos.OperatorDepartments.Responses;

public record OperatorDepartmentsResponseDto(
    OperatorDepartmentDto[] OperatorDepartments,
    int Count,
    int? LastOperatorDepartmentId);