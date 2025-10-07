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
    public async Task<IActionResult> CreateRoom([FromBody] PostRoomRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Name is required for idempotency and creation.");
        }

        var created = await roomService.CreateRoomAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetRoomById), new { roomId = created.Id }, created);
    }

    [HttpPatch("{roomId:int}")]
    public async Task<IActionResult> PatchRoom(
        int roomId,
        [FromBody] JsonPatchDocument<PatchRoomModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join("; ", errorMessage));
        }

        var current = await roomService.GetPatchModelAsync(roomId, cancellationToken);
        if (current is null)
        {
            return NotFound();
        }

        patch.ApplyTo(current);
        if (!TryValidateModel(current))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await roomService.ApplyPatchAndSaveAsync(roomId, current, cancellationToken);
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