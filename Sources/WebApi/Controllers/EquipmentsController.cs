using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Rooms.Domain.Models.Equipment;
using WebApi.ModelBinders;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/equipments")]
public class EquipmentsController(IEquipmentService equipmentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<EquipmentsResponseModel>> GetEquipments(
        [ModelBinder(BinderType = typeof(GetEquipmentsRequestModelBinder))]
        GetEquipmentsModel model,
        CancellationToken cancellationToken)
    {
        var result = await equipmentService.GetEquipmentsAsync(model, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{equipmentId:int}")]
    public async Task<ActionResult<EquipmentSchema>> GetEquipmentById(
        int equipmentId,
        CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentByIdAsync(equipmentId, cancellationToken);
        return Ok(equipment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEquipment(
        [FromBody] CreateEquipmentModel model,
        CancellationToken cancellationToken)
    {
        var created = await equipmentService.CreateEquipmentAsync(model, cancellationToken);
        return CreatedAtAction(nameof(GetEquipmentById), new { equipmentId = created.Id }, created);
    }

    [HttpPatch("{equipmentId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchEquipment(
        int equipmentId,
        [FromBody] JsonPatchDocument<PatchEquipmentModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join("; ", errorMessage));
        }

        var patchModel = await equipmentService.GetPatchModel(equipmentId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel)) return ValidationProblem(ModelState);

        var updated = await equipmentService.PatchEquipmentAsync(equipmentId, patchModel, cancellationToken);

        return Ok(updated);
    }
}