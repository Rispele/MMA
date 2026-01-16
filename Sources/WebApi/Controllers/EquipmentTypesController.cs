using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.Equipment;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.EquipmentTypes;
using WebApi.Core.Models.Responses;
using WebApi.Core.Services.Interfaces;
using WebApi.ModelBinders;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/equipment-types")]
public class EquipmentTypesController(IEquipmentTypeService equipmentTypeService) : ControllerBase
{
    /// <summary>
    /// Получить записи о типах оборудования
    /// </summary>
    /// <param name="model">Модель поиска страницы с записями</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список записей о типах оборудования</returns>
    [HttpGet]
    public async Task<ActionResult<EquipmentTypesResponseModel>> GetEquipmentTypes(
        [GetRequestWithJsonFilterModelBinder<EquipmentTypesFilterModel>]
        GetRequest<EquipmentTypesFilterModel> model,
        CancellationToken cancellationToken)
    {
        var result = await equipmentTypeService.GetEquipmentTypesAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить тип оборудования
    /// </summary>
    /// <param name="equipmentTypeId">Идентификатор типа оборудования</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Тип оборудования</returns>
    [HttpGet("{equipmentTypeId:int}")]
    public async Task<ActionResult<EquipmentTypeModel>> GetEquipmentTypeById(
        int equipmentTypeId,
        CancellationToken cancellationToken)
    {
        var equipmentType = await equipmentTypeService.GetEquipmentTypeByIdAsync(equipmentTypeId, cancellationToken);
        return Ok(equipmentType);
    }

    /// <summary>
    /// Создать тип оборудования
    /// </summary>
    /// <param name="model">Модель создания типа оборудования</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Созданный тип оборудования</returns>
    [HttpPost]
    public async Task<ActionResult<EquipmentTypeModel>> CreateEquipmentType(
        [FromBody] CreateEquipmentTypeModel model,
        CancellationToken cancellationToken)
    {
        var created = await equipmentTypeService.CreateEquipmentTypeAsync(model, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// Изменить тип оборудования
    /// </summary>
    /// <param name="equipmentTypeId">Идентификатор изменяемого типа оборудования</param>
    /// <param name="patch">Модель изменений типа оборудования</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Измененный тип оборудования</returns>
    /// <exception cref="BadHttpRequestException"></exception>
    [HttpPatch("{equipmentTypeId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<ActionResult<EquipmentTypeModel>> PatchEquipmentType(
        int equipmentTypeId,
        [FromBody] JsonPatchDocument<PatchEquipmentTypeModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await equipmentTypeService.GetEquipmentTypePatchModel(equipmentTypeId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await equipmentTypeService.PatchEquipmentTypeAsync(equipmentTypeId, patchModel, cancellationToken);

        return Ok(updated);
    }

    /// <summary>
    /// Экспортировать реестр типов оборудования
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Файл экспортированного реестра</returns>
    [HttpGet("export")]
    public async Task ExportRegistry(CancellationToken cancellationToken)
    {
        var model = await equipmentTypeService.ExportEquipmentTypeRegistry(Response.BodyWriter.AsStream(), cancellationToken);
        Response.ContentType = model.ContentType;
        Response.Headers.Append("Content-Disposition", "attachment; filename=" + Uri.EscapeDataString(model.FileName));
        model.Flush();
    }
}