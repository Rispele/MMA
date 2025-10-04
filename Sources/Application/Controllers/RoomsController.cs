using Application.Implementations.Dtos.Requests.RoomCreating;
using Application.Implementations.Dtos.Requests.RoomPatching;
using Application.Implementations.Dtos.Requests.RoomsQuerying;
using Application.Implementations.Dtos.Responses;
using Application.Implementations.Dtos.Room;
using Application.Implementations.Services.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("rooms")]
public class RoomsController(ILogger<RoomsController> logger, IRoomService roomService) : ControllerBase
{
    private readonly ILogger<RoomsController> logger = logger;

    [HttpGet("{roomId:int}")]
    public async Task<ActionResult<RoomDto>> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(roomId, cancellationToken);

        return Ok(room);
    }

    [HttpPost("filter")]
    public async Task<ActionResult<RoomsResponseDto>> GetRooms([FromBody] GetRoomsRequestDto requestDto, CancellationToken cancellationToken)
    {
        var response = await roomService.FilterRooms(requestDto, cancellationToken);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] CreateRoomRequest request, CancellationToken cancellationToken)
    {
        var createdRoom = await roomService.CreateRoom(request, cancellationToken);
        
        return Ok(createdRoom);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchRoom(int roomId, [FromBody] PatchRoomRequest patch, CancellationToken cancellationToken)
    {
        await roomService.PatchRoom(roomId, patch, cancellationToken);
        
        return Ok();
    }
}