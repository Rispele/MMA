using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Core.Dtos.OperatorDepartments.Requests;
using Rooms.Core.Dtos.OperatorDepartments.Responses;

namespace Rooms.Core.Services.OperatorDepartments.Interfaces;

public interface IOperatorDepartmentService
{
    Task<OperatorDepartmentDto> GetOperatorDepartmentById(int operatorDepartmentId, CancellationToken cancellationToken);
    Task<OperatorDepartmentDto[]> GetOperatorDepartmentsById(int[] operatorDepartmentIds, CancellationToken cancellationToken);
    Task<Dictionary<Guid, string>> GetAvailableOperators(CancellationToken cancellationToken);
    Task<OperatorDepartmentsResponseDto> FilterOperatorDepartments(GetOperatorDepartmentsDto dto, CancellationToken cancellationToken);
    Task<OperatorDepartmentDto> CreateOperatorDepartment(CreateOperatorDepartmentDto dto, CancellationToken cancellationToken);

    Task<OperatorDepartmentDto> PatchOperatorDepartment(
        int operatorDepartmentId,
        PatchOperatorDepartmentDto dto,
        CancellationToken cancellationToken);
}