using Microsoft.AspNetCore.Mvc;
using Rooms.Core.Interfaces.Dtos.Files;
using Rooms.Core.Interfaces.Services.Rooms;
using WebApi.Models.Files;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/attachments")]
public class AttachmentsController(IRoomAttachmentsService roomAttachmentsService) : ControllerBase
{
    /// <summary>
    /// Сохранить файл в хранилище
    /// </summary>
    /// <param name="id">Идентификатор файла (внутреннее название)</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentLength">Длина содержимого (заголовок)</param>
    /// <param name="content">Содержимое файла</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Модель сохраненного файла</returns>
    [HttpPost]
    [Consumes("application/octet-stream")]
    [Produces("application/json")]
    public async Task<ActionResult> StoreFile(
        [FromQuery] Guid id,
        [FromQuery] string fileName,
        [FromHeader(Name = "Content-Length")] long contentLength,
        [FromBody] Stream content,
        CancellationToken cancellationToken)
    {
        var descriptor = await roomAttachmentsService.Store(
            id,
            fileName,
            content,
            contentLength,
            cancellationToken);

        var response = new FileDescriptorModel(
            fileName,
            new FileLocationModel(
                descriptor.Location.Id,
                descriptor.Location.Bucket));

        return Ok(response);
    }

    /// <summary>
    /// Получить временную ссылку на файл-вложение
    /// </summary>
    /// <param name="id">Идентификатор файла (внутреннее название)</param>
    /// <param name="bucket">Название бакета в хранилище</param>
    /// <returns>Временная ссылка на файл</returns>
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> GetAttachment([FromQuery] Guid id, [FromQuery] string bucket)
    {
        if (id == Guid.Empty || string.IsNullOrWhiteSpace(bucket))
        {
            return BadRequest();
        }

        var locationToLoad = new FileLocationDto(id, bucket);
        var file = await roomAttachmentsService.Load(locationToLoad);
        if (file is null)
        {
            return NotFound();
        }

        return Ok(file);
    }
}