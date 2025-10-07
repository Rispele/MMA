using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.ModelBinders;
using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/rooms")]
public class RoomsController(
    IRoomService roomService,
    IFileService fileService,
    ILogger<RoomsController> logger)
    : ControllerBase
{
    private readonly ILogger<RoomsController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult<RoomsResponse>> GetRooms(
        [ModelBinder(BinderType = typeof(GetRoomsRequestModelBinder))]
        RoomsRequest request,
        CancellationToken cancellationToken)
    {
        var result = await roomService.GetRoomsAsync(request, cancellationToken);

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
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            return BadRequest("Name is required for idempotency and creation.");
        }

        var created = await roomService.CreateRoomAsync(model, cancellationToken);

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
        
        var patchModel = await roomService.GetPatchModel(roomId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }
        
        var updated = await roomService.PatchRoomAsync(roomId, patchModel, cancellationToken);

        return Ok(updated);
        
    }

    [HttpGet("/webapi/attachments")]
    public async Task<IActionResult> GetAttachment([FromQuery] Guid id, [FromQuery] string bucket, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty || string.IsNullOrWhiteSpace(bucket)) return BadRequest();

        var file = await fileService.GetFileAsync(id, cancellationToken);
        if (file is null) return NotFound();

        file.FileName = id.ToString();

        return File(file.Stream, file.ContentType ?? "application/octet-stream", file.FileName);
    }
}