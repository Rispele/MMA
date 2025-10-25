using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.ModelBinders;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Responses;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/rooms")]
public class RoomsController(IRoomService roomService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<RoomsResponseModel>> GetRooms(
        [ModelBinder(BinderType = typeof(GetRoomsRequestModelBinder))]
        GetRoomsModel model,
        CancellationToken cancellationToken)
    {
        var result = await roomService.GetRoomsAsync(model, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{roomId:int}")]
    public async Task<ActionResult<RoomModel>> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomByIdAsync(roomId, cancellationToken);
        return Ok(room);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomModel model, CancellationToken cancellationToken)
    {
        var created = await roomService.CreateRoom(model, cancellationToken);

        return CreatedAtAction(nameof(GetRoomById), new { roomId = created.Id }, created);
    }

    [HttpPatch("{roomId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchRoom(
        int roomId,
        [FromBody] JsonPatchDocument<PatchRoomModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join("; ", errorMessage));
        }

        var (model, isOk) = await roomService.PatchRoomAsync(roomId, patch, TryValidateModel, cancellationToken);

        return isOk 
            ? ValidationProblem(ModelState) 
            : Ok(model);
    }
}