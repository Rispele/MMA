using WebApi.Models.OperatorDepartments;
using WebApi.Models.Requests.OperatorDepartments;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IOperatorDepartmentService
{
    Task<OperatorDepartmentsResponseModel> GetOperatorDepartmentsAsync(GetOperatorDepartmentsModel model, CancellationToken cancellationToken);
    Task<OperatorDepartmentModel> GetOperatorDepartmentByIdAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableOperatorsAsync(CancellationToken cancellationToken); // ответ - список моделей
    Task<OperatorDepartmentModel> CreateOperatorDepartmentAsync(CreateOperatorDepartmentModel model, CancellationToken cancellationToken);
    Task<PatchOperatorDepartmentModel> GetOperatorDepartmentPatchModel(int operatorDepartmentId, CancellationToken cancellationToken);

    Task<OperatorDepartmentModel> PatchOperatorDepartmentAsync(
        int operatorDepartmentId,
        PatchOperatorDepartmentModel request,
        CancellationToken cancellationToken);
}