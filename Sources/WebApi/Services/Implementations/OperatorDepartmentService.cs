using WebApi.ModelConverters;
using WebApi.Models.OperatorDepartments;
using WebApi.Models.Requests.OperatorDepartments;
using WebApi.Models.Responses;
using IOperatorDepartmentService = WebApi.Services.Interfaces.IOperatorDepartmentService;

namespace WebApi.Services.Implementations;

public class OperatorDepartmentService(Rooms.Core.Services.Interfaces.IOperatorDepartmentService operatorDepartmentService) : IOperatorDepartmentService
{
    public async Task<OperatorDepartmentsResponseModel> GetOperatorDepartmentsAsync(
        GetOperatorDepartmentsModel model,
        CancellationToken cancellationToken)
    {
        var getOperatorDepartmentsRequest = OperatorDepartmentsModelsConverter.Convert(model);

        var batch = await operatorDepartmentService.FilterOperatorDepartments(getOperatorDepartmentsRequest, cancellationToken);

        return new OperatorDepartmentsResponseModel
        {
            OperatorDepartments = batch.OperatorDepartments.Select(OperatorDepartmentsModelsConverter.Convert).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<OperatorDepartmentModel> GetOperatorDepartmentByIdAsync(int id, CancellationToken cancellationToken)
    {
        var operatorDepartment = await operatorDepartmentService.GetOperatorDepartmentById(id, cancellationToken);

        return OperatorDepartmentsModelsConverter.Convert(operatorDepartment);
    }

    public async Task<Dictionary<Guid, string>> GetAvailableOperatorsAsync(CancellationToken cancellationToken)
    {
        var operators = await operatorDepartmentService.GetAvailableOperators(cancellationToken);

        return operators;
    }

    public async Task<OperatorDepartmentModel> CreateOperatorDepartmentAsync(
        CreateOperatorDepartmentModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = OperatorDepartmentsModelsConverter.Convert(model);

        var operatorDepartment = await operatorDepartmentService.CreateOperatorDepartment(innerRequest, cancellationToken);

        return OperatorDepartmentsModelsConverter.Convert(operatorDepartment);
    }

    public async Task<PatchOperatorDepartmentModel> GetOperatorDepartmentPatchModel(int operatorDepartmentId, CancellationToken cancellationToken)
    {
        var operatorDepartment = await operatorDepartmentService.GetOperatorDepartmentById(operatorDepartmentId, cancellationToken);

        return OperatorDepartmentsModelsConverter.ConvertToPatchModel(operatorDepartment);
    }

    public async Task<OperatorDepartmentModel> PatchOperatorDepartmentAsync(
        int operatorDepartmentId,
        PatchOperatorDepartmentModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = OperatorDepartmentsModelsConverter.Convert(patchModel);

        var patched = await operatorDepartmentService.PatchOperatorDepartment(operatorDepartmentId, patchRequest, cancellationToken);

        return OperatorDepartmentsModelsConverter.Convert(patched);
    }
}