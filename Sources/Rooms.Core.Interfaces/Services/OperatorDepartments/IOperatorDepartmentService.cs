using Rooms.Core.Interfaces.Dtos.OperatorDepartments;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Responses;
using Commons.ExternalClients.LkUsers;

namespace Rooms.Core.Interfaces.Services.OperatorDepartments;

public interface IOperatorDepartmentService
{
    Task<OperatorDepartmentDto> GetOperatorDepartmentById(int operatorDepartmentId, CancellationToken cancellationToken);
    Task<OperatorDepartmentDto[]> GetOperatorDepartmentsById(int[] operatorDepartmentIds, CancellationToken cancellationToken);
    Task<LkEmployeeDto[]> GetAvailableOperators(CancellationToken cancellationToken);
    Task<OperatorDepartmentsResponseDto> FilterOperatorDepartments(GetOperatorDepartmentsDto dto, CancellationToken cancellationToken);
    Task<OperatorDepartmentDto> CreateOperatorDepartment(CreateOperatorDepartmentDto dto, CancellationToken cancellationToken);

    Task<OperatorDepartmentDto> PatchOperatorDepartment(
        int operatorDepartmentId,
        PatchOperatorDepartmentDto dto,
        CancellationToken cancellationToken);
}