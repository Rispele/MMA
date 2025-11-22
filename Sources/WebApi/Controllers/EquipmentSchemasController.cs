using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WebApi.ModelBinders;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/equipment-schemas")]
public class EquipmentSchemasController(IEquipmentSchemaService equipmentSchemaService) : ControllerBase
{
    /// <summary>
    /// Получить записи о моделях оборудования
    /// </summary>
    /// <param name="model">Модель поиска страницы с записями</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список записей о моделях оборудования</returns>
    [HttpGet]
    public async Task<ActionResult<EquipmentSchemasResponseModel>> GetEquipmentSchemas(
        [ModelBinder(BinderType = typeof(GetEquipmentSchemasRequestModelBinder))]
        GetEquipmentSchemasModel model,
        CancellationToken cancellationToken)
    {
        var result = await equipmentSchemaService.GetEquipmentSchemasAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить модель оборудования
    /// </summary>
    /// <param name="equipmentSchemaId">Идентификатор модели</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Модлель оборудования</returns>
    [HttpGet("{equipmentSchemaId:int}")]
    public async Task<ActionResult<EquipmentSchemaModel>> GetEquipmentSchemaById(
        int equipmentSchemaId,
        CancellationToken cancellationToken)
    {
        var equipmentSchema = await equipmentSchemaService.GetEquipmentSchemaByIdAsync(equipmentSchemaId, cancellationToken);
        return Ok(equipmentSchema);
    }

    /// <summary>
    /// Создать модель оборудования
    /// </summary>
    /// <param name="model">Модель создания модели оборудования</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Созданная модель оборудования</returns>
    [HttpPost]
    public async Task<IActionResult> CreateEquipmentSchema(
        [FromBody] CreateEquipmentSchemaModel model,
        CancellationToken cancellationToken)
    {
        var created = await equipmentSchemaService.CreateEquipmentSchemaAsync(model, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// Изменить модель оборудования
    /// </summary>
    /// <param name="equipmentSchemaId">Идентификатор изменяемой модели оборудования</param>
    /// <param name="patch">Модель изменений модели оборудования</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Измененная модель оборудования</returns>
    /// <exception cref="BadHttpRequestException"></exception>
    [HttpPatch("{equipmentSchemaId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchEquipmentSchema(
        int equipmentSchemaId,
        [FromBody] JsonPatchDocument<PatchEquipmentSchemaModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await equipmentSchemaService.GetEquipmentSchemaPatchModel(equipmentSchemaId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await equipmentSchemaService.PatchEquipmentSchemaAsync(equipmentSchemaId, patchModel, cancellationToken);

        return Ok(updated);
    }

    /// <summary>
    /// Экспортировать реестр моделей оборудования
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Файл экспортированного реестра</returns>
    [HttpGet("export")]
    public async Task<FileStreamResult> ExportRegistry(CancellationToken cancellationToken)
    {
        var model = await equipmentSchemaService.ExportEquipmentSchemaRegistry(cancellationToken);
        return new FileStreamResult(model.Content, new MediaTypeHeaderValue(model.ContentType))
        {
            FileDownloadName = model.FileName
        };
    }
}