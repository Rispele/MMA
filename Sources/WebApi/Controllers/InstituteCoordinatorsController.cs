using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.InstituteCoordinator;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.InstituteCoordinators;
using WebApi.Core.Models.Responses;
using WebApi.Core.Services.Interfaces;
using WebApi.ModelBinders;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/institute-coordinator")]
public class InstituteCoordinatorsController(IInstituteCoordinatorService instituteCoordinatorService) : ControllerBase
{
    /// <summary>
    /// Получить записи об ответственных от институтов/подразделений
    /// </summary>
    /// <param name="model">Модель поиска страницы с записями</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список записей об ответсвенных от институтов/подразделений</returns>
    [HttpGet]
    public async Task<ActionResult<InstituteCoordinatorsResponseModel>> GetInstituteCoordinator(
        [GetRequestWithJsonFilterModelBinder<InstituteCoordinatorsFilterModel>]
        GetRequest<InstituteCoordinatorsFilterModel> model,
        CancellationToken cancellationToken)
    {
        var result = await instituteCoordinatorService.GetInstituteCoordinatorsAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить ответственного от института/подразделения
    /// </summary>
    /// <param name="instituteCoordinatorId">Идентификатор ответственного</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Ответственный от института/подразделения</returns>
    [HttpGet("{instituteCoordinatorId:int}")]
    public async Task<ActionResult<InstituteCoordinatorModel>> GetInstituteCoordinatorById(
        int instituteCoordinatorId,
        CancellationToken cancellationToken)
    {
        var instituteCoordinator = await instituteCoordinatorService.GetInstituteCoordinatorByIdAsync(instituteCoordinatorId, cancellationToken);
        return Ok(instituteCoordinator);
    }

    /// <summary>
    /// Получить список доступных для выбора ответственных лиц
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных для выбора ответственных лиц</returns>
    [HttpGet("coordinators")]
    public async Task<ActionResult<InstituteCoordinatorEmployeeModel[]>> GetAvailableCoordinators(CancellationToken cancellationToken)
    {
        var coordinators = await instituteCoordinatorService.GetAvailableInstituteCoordinatorsAsync(cancellationToken);
        return Ok(coordinators);
    }

    /// <summary>
    /// Получить список доступных для выбора институтов/подразделений
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных для выбора институтов/подразделений</returns>
    [HttpGet("departments")]
    public async Task<ActionResult<InstituteDepartmentResponseModel[]>> GetAvailableDepartments(CancellationToken cancellationToken)
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
    public async Task<ActionResult<InstituteCoordinatorModel>> CreateInstituteCoordinator(
        [FromBody] CreateInstituteCoordinatorModel model,
        CancellationToken cancellationToken)
    {
        var created = await instituteCoordinatorService.CreateInstituteCoordinatorAsync(model, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// Изменить ответственного от института/подразделения
    /// </summary>
    /// <param name="instituteCoordinatorId">Идентификатор ответственного</param>
    /// <param name="patch">Модель изменений ответственного</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Измененный ответственный от института/подразделения</returns>
    /// <exception cref="BadHttpRequestException"></exception>
    [HttpPatch("{instituteCoordinatorId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<ActionResult<InstituteCoordinatorModel>> PatchInstituteCoordinator(
        int instituteCoordinatorId,
        [FromBody] JsonPatchDocument<PatchInstituteCoordinatorModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await instituteCoordinatorService.GetInstituteCoordinatorPatchModel(instituteCoordinatorId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await instituteCoordinatorService.PatchInstituteCoordinatorAsync(instituteCoordinatorId, patchModel, cancellationToken);

        return Ok(updated);
    }
}