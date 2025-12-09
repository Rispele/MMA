using Booking.Domain.Models.InstituteCoordinators;
using Commons.ExternalClients.LkUsers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.ModelBinders;
using WebApi.Models.Requests;
using WebApi.Models.Requests.InstituteResponsible;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/institute-coordinator")]
public class InstituteCoordinatorsController(IInstituteResponsibleService instituteCoordinatorService) : ControllerBase
{
    /// <summary>
    /// Получить записи об ответственных от институтов/подразделений
    /// </summary>
    /// <param name="model">Модель поиска страницы с записями</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список записей об ответсвенных от институтов/подразделений</returns>
    [HttpGet]
    public async Task<ActionResult<InstituteResponsibleResponseModel>> GetInstituteCoordinator(
        [GetRequestWithJsonFilterModelBinder<InstituteCoordinatorFilterModel>]
        GetRequest<InstituteCoordinatorFilterModel> model,
        CancellationToken cancellationToken)
    {
        var result = await instituteCoordinatorService.GetInstituteResponsibleAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить ответственного от института/подразделения
    /// </summary>
    /// <param name="instituteResponsibleId">Идентификатор ответственного</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Ответственный от института/подразделения</returns>
    [HttpGet("{instituteResponsibleId:int}")]
    public async Task<ActionResult<InstituteCoordinator>> GetInstituteCoordinatorById(
        int instituteResponsibleId,
        CancellationToken cancellationToken)
    {
        var instituteResponsible = await instituteCoordinatorService.GetInstituteResponsibleByIdAsync(instituteResponsibleId, cancellationToken);
        return Ok(instituteResponsible);
    }

    /// <summary>
    /// Получить список доступных для выбора ответственных лиц
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных для выбора ответственных лиц</returns>
    [HttpGet("responsible")]
    public async Task<ActionResult<Task<LkEmployeeDto[]>>> GetAvailableCoordinators(CancellationToken cancellationToken)
    {
        var responsible = await instituteCoordinatorService.GetAvailableInstituteResponsibleAsync(cancellationToken);
        return Ok(responsible);
    }

    /// <summary>
    /// Получить список доступных для выбора институтов/подразделений
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных для выбора институтов/подразделений</returns>
    [HttpGet("departments")]
    public async Task<ActionResult<Dictionary<string, string>>> GetAvailableDepartments(CancellationToken cancellationToken)
    {
        var departments = await instituteCoordinatorService.GetAvailableInstituteDepartmentsAsync(cancellationToken);
        return Ok(departments);
    }

    /// <summary>
    /// Создать ответственного от института/подразделения
    /// </summary>
    /// <param name="model">Модель создания ответственного</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Созданный ответственный от института/подразделения</returns>
    [HttpPost]
    public async Task<IActionResult> CreateInstituteCoordinator(
        [FromBody] CreateInstituteCoordinatorModel model,
        CancellationToken cancellationToken)
    {
        var created = await instituteCoordinatorService.CreateInstituteResponsibleAsync(model, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// Изменить ответственного от института/подразделения
    /// </summary>
    /// <param name="instituteResponsibleId">Идентификатор ответственного</param>
    /// <param name="patch">Модель изменений ответственного</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Измененный ответственный от института/подразделения</returns>
    /// <exception cref="BadHttpRequestException"></exception>
    [HttpPatch("{instituteResponsibleId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchInstituteCoordinator(
        int instituteResponsibleId,
        [FromBody] JsonPatchDocument<PatchInstituteCoordinatorModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await instituteCoordinatorService.GetInstituteResponsiblePatchModel(instituteResponsibleId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await instituteCoordinatorService.PatchInstituteResponsibleAsync(instituteResponsibleId, patchModel, cancellationToken);

        return Ok(updated);
    }
}