using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.Requests.RoomSchedule;
using WebApi.Core.Models.Responses;
using WebApi.Core.Models.RoomSchedule;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/room-schedule")]
public class RoomScheduleController(
    IRoomScheduleService roomScheduleService,
    IRoomService roomService) : ControllerBase
{
    /// <summary>
    /// Выполнить автокомплит для названия аудитории
    /// </summary>
    /// <param name="roomName">Часть названия аудитории</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список аудиторий, для которых выполнился автокомплит</returns>
    [HttpGet("autocomplete")]
    public async Task<ActionResult<IEnumerable<AutocompleteRoomResponseModel>>> AutocompleteRoom(
        [FromQuery] string roomName,
        CancellationToken cancellationToken)
    {
        var result = await roomService.AutocompleteRoomAsync(roomName, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить расписание для данной аудитории на дату
    /// </summary>
    /// <param name="roomId">Идентификатор аудитории</param>
    /// <param name="date">Дата поиска расписания</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список занятий в выбранной аудитории на дату</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomScheduleModel>>> GetRoomSchedule(
        [FromQuery] int roomId,
        [FromQuery] DateOnly date,
        CancellationToken cancellationToken)
    {
        var model = new GetRoomScheduleModel(roomId, From: date, To: date.AddDays(1));
        var result = await roomScheduleService.GetRoomSchedule(model, cancellationToken);
        return Ok(result);
    }
}