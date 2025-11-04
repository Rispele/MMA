using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WebApi.ModelBinders;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/equipment-types")]
public class EquipmentTypesController(IEquipmentTypeService equipmentTypeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<EquipmentTypesResponseModel>> GetEquipmentTypes(
        [ModelBinder(BinderType = typeof(GetEquipmentTypesRequestModelBinder))]
        GetEquipmentTypesModel model,
        CancellationToken cancellationToken)
    {
        var result = await equipmentTypeService.GetEquipmentTypesAsync(model, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{equipmentTypeId:int}")]
    public async Task<ActionResult<EquipmentTypeModel>> GetEquipmentTypeById(
        int equipmentTypeId,
        CancellationToken cancellationToken)
    {
        var equipmentType = await equipmentTypeService.GetEquipmentTypeByIdAsync(equipmentTypeId, cancellationToken);
        return Ok(equipmentType);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEquipmentType(
        [FromBody] CreateEquipmentTypeModel model,
        CancellationToken cancellationToken)
    {
        var created = await equipmentTypeService.CreateEquipmentTypeAsync(model, cancellationToken);
        return Ok(created);
    }

    [HttpPatch("{equipmentTypeId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchEquipmentType(
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

    [HttpGet("export")]
    public async Task<FileStreamResult> ExportRegistry(CancellationToken cancellationToken)
    {
        var model = await equipmentTypeService.ExportEquipmentTypeRegistry(cancellationToken);
        return new FileStreamResult(model.Content, new MediaTypeHeaderValue(model.ContentType))
        {
            FileDownloadName = model.FileName,
        };
    }
}