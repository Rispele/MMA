using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests.RoomSchedule;
using WebApi.Models.RoomSchedule;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/room-schedule")]
public class RoomScheduleController(IRoomScheduleService roomScheduleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomScheduleModel>>> GetInstituteResponsible(
        [FromBody] GetRoomScheduleModel model,
        CancellationToken cancellationToken)
    {
        var result = await roomScheduleService.GetRoomSchedule(model);
        return Ok(result);
    }
}
    