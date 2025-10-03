using Application.Implementations.Dtos.Requests.RoomCreating;
using Application.Implementations.Dtos.Requests.RoomPatching;
using Application.Implementations.Dtos.Requests.RoomsQuerying;
using Application.Implementations.Dtos.Responses;
using Application.Implementations.Dtos.Room;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("webapi/rooms")]
public class RoomsController(ILogger<RoomsController> logger) : ControllerBase
{
    private readonly ILogger<RoomsController> logger = logger;

    [HttpGet("{roomId:int}")]
    public async Task<ActionResult<RoomDto>> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost]
    [Route("filter")]
    public async Task<ActionResult<RoomsResponseDto>> GetRooms([FromBody] RoomsRequestDto requestDto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] PostRoomRequest request, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPatch("{roomId:int}")]
    public async Task<IActionResult> PatchRoom(int roomId, [FromBody] PatchRoomRequestDto patch, CancellationToken cancellationToken)
    {
        return Ok();
    }
}