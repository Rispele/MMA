using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests.RoomSchedule;
using WebApi.Models.RoomSchedule;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/room-schedule")]
public class RoomScheduleController(IRoomScheduleService roomScheduleService) : ControllerBase
{
    /// <summary>
    /// Получить расписание для данной аудитории на дату
    /// </summary>
    /// <param name="roomId">Идентификатор аудитории</param>
    /// <param name="date">Дата поиска расписания</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список занятий в выбранной аудитории на дату</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomScheduleModel>>> GetInstituteResponsible(
        [FromQuery] int roomId,
        [FromQuery] DateOnly date,
        CancellationToken cancellationToken)
    {
        var model = new GetRoomScheduleModel
        {
            RoomId = roomId,
            Date = date,
        };
        var result = await roomScheduleService.GetRoomSchedule(model, cancellationToken);
        return Ok(result);
    }
}
    