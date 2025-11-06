using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.ModelBinders;
using WebApi.Models.OperatorDepartments;
using WebApi.Models.Requests.OperatorDepartments;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/operator-departments")]
public class OperatorDepartmentsController(IOperatorDepartmentService operatorDepartmentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OperatorDepartmentsResponseModel>> GetOperatorDepartments(
        [ModelBinder(BinderType = typeof(GetOperatorDepartmentsRequestModelBinder))]
        GetOperatorDepartmentsModel model,
        CancellationToken cancellationToken)
    {
        var result = await operatorDepartmentService.GetOperatorDepartmentsAsync(model, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OperatorDepartmentModel>> GetOperatorDepartmentById(
        int id,
        CancellationToken cancellationToken)
    {
        var operatorDepartments = await operatorDepartmentService.GetOperatorDepartmentByIdAsync(id, cancellationToken);
        return Ok(operatorDepartments);
    }

    [HttpGet("operators")]
    public async Task<ActionResult<Dictionary<Guid, string>>> GetAvailableOperators(CancellationToken cancellationToken)
    {
        var operators = await operatorDepartmentService.GetAvailableOperatorsAsync(cancellationToken);
        return Ok(operators);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOperatorDepartment(
        [FromBody] CreateOperatorDepartmentModel model,
        CancellationToken cancellationToken)
    {
        var created = await operatorDepartmentService.CreateOperatorDepartmentAsync(model, cancellationToken);
        return Ok(created);
    }

    [HttpPatch("{id:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchOperatorDepartment(
        int id,
        [FromBody] JsonPatchDocument<PatchOperatorDepartmentModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await operatorDepartmentService.GetOperatorDepartmentPatchModel(id, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await operatorDepartmentService.PatchOperatorDepartmentAsync(id, patchModel, cancellationToken);

        return Ok(updated);
    }
}