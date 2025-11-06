using Rooms.Core.Dtos.OperatorDepartments;

namespace Rooms.Core.Dtos.Responses;

public record OperatorDepartmentsResponseDto(
    OperatorDepartmentDto[] OperatorDepartments,
    int Count,
    int? LastOperatorDepartmentId);