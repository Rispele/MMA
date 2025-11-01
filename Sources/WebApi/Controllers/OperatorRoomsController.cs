using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.ModelBinders;
using WebApi.Models.OperatorRoom;
using WebApi.Models.Requests.OperatorRooms;
using WebApi.Models.Responses;
using IOperatorRoomService = WebApi.Services.Interfaces.IOperatorRoomService;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/operator-rooms")]
public class OperatorRoomsController(IOperatorRoomService operatorRoomService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OperatorRoomsResponseModel>> GetOperatorRooms(
        [ModelBinder(BinderType = typeof(GetOperatorRoomsRequestModelBinder))]
        GetOperatorRoomsModel model,
        CancellationToken cancellationToken)
    {
        var result = await operatorRoomService.GetOperatorRoomsAsync(model, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{operatorRoomId:int}")]
    public async Task<ActionResult<OperatorRoomModel>> GetOperatorRoomById(
        int operatorRoomId,
        CancellationToken cancellationToken)
    {
        var operatorRoom = await operatorRoomService.GetOperatorRoomByIdAsync(operatorRoomId, cancellationToken);
        return Ok(operatorRoom);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOperatorRoom(
        [FromBody] CreateOperatorRoomModel model,
        CancellationToken cancellationToken)
    {
        var created = await operatorRoomService.CreateOperatorRoomAsync(model, cancellationToken);
        return Ok(created);
    }

    [HttpPatch("{operatorRoomId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchOperatorRoom(
        int operatorRoomId,
        [FromBody] JsonPatchDocument<PatchOperatorRoomModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await operatorRoomService.GetOperatorRoomPatchModel(operatorRoomId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await operatorRoomService.PatchOperatorRoomAsync(operatorRoomId, patchModel, cancellationToken);

        return Ok(updated);
    }
}