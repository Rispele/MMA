using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.Rooms;
using WebApi.Core.Models.Responses;
using WebApi.Core.Models.Room;
using WebApi.Core.Services.Interfaces;
using WebApi.ModelBinders;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/rooms")]
public class RoomsController(IRoomService roomService) : ControllerBase
{
    /// <summary>
    /// Получить записи об аудиториях
    /// </summary>
    /// <param name="model">Модель поиска страницы с записями</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список записей об аудиториях</returns>
    [HttpGet]
    public async Task<ActionResult<RoomsResponseModel>> GetRooms(
        [GetRequestWithJsonFilterModelBinder<RoomsFilterModel>]
        GetRequest<RoomsFilterModel> model,
        CancellationToken cancellationToken)
    {
        var result = await roomService.GetRoomsAsync(model, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Получить аудиторию
    /// </summary>
    /// <param name="roomId">Идентификатор аудитории</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Аудитория</returns>
    [HttpGet("{roomId:int}")]
    public async Task<ActionResult<RoomModel>> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomByIdAsync(roomId, cancellationToken);
        return Ok(room);
    }

    /// <summary>
    /// Создать аудиторию
    /// </summary>
    /// <param name="model">Модель создания аудитории</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Созданная аудитория</returns>
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomModel model, CancellationToken cancellationToken)
    {
        var created = await roomService.CreateRoom(model, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// Изменить аудиторию
    /// </summary>
    /// <param name="roomId">Идентификатор изменяемой аудитории</param>
    /// <param name="patch">Модель изменений аудитории</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Измененная модель аудитории</returns>
    /// <exception cref="BadHttpRequestException"></exception>
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
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var (model, isOk) = await roomService.PatchRoomAsync(roomId, patch, TryValidateModel, cancellationToken);

        return isOk
            ? ValidationProblem(ModelState)
            : Ok(model);
    }

    /// <summary>
    /// Экспортировать реестр аудиторий
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Файл экспортированного реестра</returns>
    [HttpGet("export")]
    public async Task ExportRegistry(CancellationToken cancellationToken)
    {
        var model = await roomService.ExportRoomRegistry(Response.BodyWriter.AsStream(), cancellationToken);
        Response.ContentType = model.ContentType;
        Response.Headers.Append("Content-Disposition", "attachment; filename=" + Uri.EscapeDataString(model.FileName));
        model.Flush();
    }
}