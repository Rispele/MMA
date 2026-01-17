using WebApi.Core.Models.OperatorDepartments;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.OperatorDepartments;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Interfaces;

public interface IOperatorDepartmentService
{
    Task<OperatorDepartmentsResponseModel> GetOperatorDepartmentsAsync(
        GetRequest<OperatorDepartmentsFilterModel> model,
        CancellationToken cancellationToken);

    Task<OperatorDepartmentModel> GetOperatorDepartmentByIdAsync(int id, CancellationToken cancellationToken);
    Task<OperatorModel[]> GetAvailableOperatorsAsync(CancellationToken cancellationToken);
    Task<OperatorDepartmentModel> CreateOperatorDepartmentAsync(CreateOperatorDepartmentModel model, CancellationToken cancellationToken);
    Task<PatchOperatorDepartmentModel> GetOperatorDepartmentPatchModel(int operatorDepartmentId, CancellationToken cancellationToken);

    Task<OperatorDepartmentModel> PatchOperatorDepartmentAsync(
        int operatorDepartmentId,
        PatchOperatorDepartmentModel request,
        CancellationToken cancellationToken);
}