using Commons.ExternalClients.LkUsers;
using WebApi.Core.ModelConverters;
using WebApi.Core.Models.OperatorDepartments;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.OperatorDepartments;
using WebApi.Core.Models.Responses;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Core.Services.Implementations;

public class OperatorDepartmentService(Rooms.Core.Interfaces.Services.OperatorDepartments.IOperatorDepartmentService operatorDepartmentService)
    : IOperatorDepartmentService
{
    public async Task<OperatorDepartmentsResponseModel> GetOperatorDepartmentsAsync(
        GetRequest<OperatorDepartmentsFilterModel> model,
        CancellationToken cancellationToken)
    {
        var getOperatorDepartmentsRequest = OperatorDepartmentsModelsMapper.Convert(model);

        var batch = await operatorDepartmentService.FilterOperatorDepartments(getOperatorDepartmentsRequest, cancellationToken);

        return new OperatorDepartmentsResponseModel
        {
            OperatorDepartments = batch.OperatorDepartments.Select(OperatorDepartmentsModelsMapper.Convert).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<OperatorDepartmentModel> GetOperatorDepartmentByIdAsync(int id, CancellationToken cancellationToken)
    {
        var operatorDepartment = await operatorDepartmentService.GetOperatorDepartmentById(id, cancellationToken);

        return OperatorDepartmentsModelsMapper.Convert(operatorDepartment);
    }

    public async Task<LkEmployeeDto[]> GetAvailableOperatorsAsync(CancellationToken cancellationToken)
    {
        var operators = await operatorDepartmentService.GetAvailableOperators(cancellationToken);

        return operators;
    }

    public async Task<OperatorDepartmentModel> CreateOperatorDepartmentAsync(
        CreateOperatorDepartmentModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = OperatorDepartmentsModelsMapper.Convert(model);

        var operatorDepartment = await operatorDepartmentService.CreateOperatorDepartment(innerRequest, cancellationToken);

        return OperatorDepartmentsModelsMapper.Convert(operatorDepartment);
    }

    public async Task<PatchOperatorDepartmentModel> GetOperatorDepartmentPatchModel(int operatorDepartmentId, CancellationToken cancellationToken)
    {
        var operatorDepartment = await operatorDepartmentService.GetOperatorDepartmentById(operatorDepartmentId, cancellationToken);

        return OperatorDepartmentsModelsMapper.ConvertToPatchModel(operatorDepartment);
    }

    public async Task<OperatorDepartmentModel> PatchOperatorDepartmentAsync(
        int operatorDepartmentId,
        PatchOperatorDepartmentModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = OperatorDepartmentsModelsMapper.Convert(patchModel);

        var patched = await operatorDepartmentService.PatchOperatorDepartment(operatorDepartmentId, patchRequest, cancellationToken);

        return OperatorDepartmentsModelsMapper.Convert(patched);
    }
}