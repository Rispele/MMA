using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Rooms.Domain.Models.InstituteResponsible;
using WebApi.ModelBinders;
using WebApi.Models.Requests.InstituteResponsible;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/institute-responsible")]
public class InstituteResponsibleController(IInstituteResponsibleService instituteResponsibleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<InstituteResponsibleResponseModel>> GetInstituteResponsible(
        [ModelBinder(BinderType = typeof(GetInstituteResponsibleRequestModelBinder))]
        GetInstituteResponsibleModel model,
        CancellationToken cancellationToken)
    {
        var result = await instituteResponsibleService.GetInstituteResponsibleAsync(model, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{instituteResponsibleId:int}")]
    public async Task<ActionResult<InstituteResponsible>> GetInstituteResponsibleById(
        int instituteResponsibleId,
        CancellationToken cancellationToken)
    {
        var instituteResponsible = await instituteResponsibleService.GetInstituteResponsibleByIdAsync(instituteResponsibleId, cancellationToken);
        return Ok(instituteResponsible);
    }

    [HttpGet("responsible")]
    public async Task<ActionResult<Dictionary<Guid, string>>> GetAvailableResponsible(CancellationToken cancellationToken)
    {
        var responsible = await instituteResponsibleService.GetAvailableInstituteResponsibleAsync(cancellationToken);
        return Ok(responsible);
    }

    [HttpGet("departments")]
    public async Task<ActionResult<Dictionary<Guid, string>>> GetAvailableDepartments(CancellationToken cancellationToken)
    {
        var departments = await instituteResponsibleService.GetAvailableInstituteDepartmentsAsync(cancellationToken);
        return Ok(departments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInstituteResponsible(
        [FromBody] CreateInstituteResponsibleModel model,
        CancellationToken cancellationToken)
    {
        var created = await instituteResponsibleService.CreateInstituteResponsibleAsync(model, cancellationToken);
        return Ok(created);
    }

    [HttpPatch("{instituteResponsibleId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchInstituteResponsible(
        int instituteResponsibleId,
        [FromBody] JsonPatchDocument<PatchInstituteResponsibleModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await instituteResponsibleService.GetInstituteResponsiblePatchModel(instituteResponsibleId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await instituteResponsibleService.PatchInstituteResponsibleAsync(instituteResponsibleId, patchModel, cancellationToken);

        return Ok(updated);
    }
}