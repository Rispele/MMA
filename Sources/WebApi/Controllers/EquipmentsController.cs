using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.Equipment;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.Equipments;
using WebApi.Core.Models.Responses;
using WebApi.Core.Services.Interfaces;
using WebApi.ModelBinders;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/equipments")]
public class EquipmentsController(IEquipmentService equipmentService) : ControllerBase
{
    /// <summary>
    /// Получить записи о единицах оборудования
    /// </summary>
    /// <param name="model">Модель поиска страницы с записями</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список записей о единицах оборудования</returns>
    [HttpGet]
    public async Task<ActionResult<EquipmentsResponseModel>> GetEquipments(
        [GetRequestWithJsonFilterModelBinder<EquipmentsFilterModel>]
        GetRequest<EquipmentsFilterModel> model,
        CancellationToken cancellationToken)
    {
        var result = await equipmentService.GetEquipmentsAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить единицу оборудования
    /// </summary>
    /// <param name="equipmentId">Идентификатор единицы оборудования</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Единица оборудования</returns>
    [HttpGet("{equipmentId:int}")]
    public async Task<ActionResult<EquipmentModel>> GetEquipmentById(
        int equipmentId,
        CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentByIdAsync(equipmentId, cancellationToken);
        return Ok(equipment);
    }

    /// <summary>
    /// Создать единицу оборудования
    /// </summary>
    /// <param name="model">Модель создания единицы оборудования</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Созданная единица оборудования</returns>
    [HttpPost]
    public async Task<ActionResult<EquipmentModel>> CreateEquipment(
        [FromBody] CreateEquipmentModel model,
        CancellationToken cancellationToken)
    {
        var created = await equipmentService.CreateEquipmentAsync(model, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// Изменить единицу оборудования
    /// </summary>
    /// <param name="equipmentId">Идентификатор изменяемой единицы оборудования</param>
    /// <param name="patch">Модель изменений единицы оборудования</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Измененная единица оборудования</returns>
    /// <exception cref="BadHttpRequestException"></exception>
    [HttpPatch("{equipmentId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<ActionResult<EquipmentModel>> PatchEquipment(
        int equipmentId,
        [FromBody] JsonPatchDocument<PatchEquipmentModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await equipmentService.GetEquipmentPatchModel(equipmentId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await equipmentService.PatchEquipmentAsync(equipmentId, patchModel, cancellationToken);

        return Ok(updated);
    }

    /// <summary>
    /// Экспортировать реестр единиц оборудования
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Файл экспортированного реестра</returns>
    [HttpGet("export")]
    public async Task ExportRegistry(CancellationToken cancellationToken)
    {
        var model = await equipmentService.ExportEquipmentRegistry(Response.BodyWriter.AsStream(), cancellationToken);
        Response.ContentType = model.ContentType;
        Response.Headers.Append("Content-Disposition", "attachment; filename=" + Uri.EscapeDataString(model.FileName));
        model.Flush();
    }
}