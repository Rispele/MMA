using Microsoft.AspNetCore.Mvc;
using Rooms.Core.Interfaces.Dtos.Files;
using Rooms.Core.Interfaces.Services.Rooms;
using WebApi.Core.Models.Files;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/attachments")]
public class AttachmentsController(IRoomAttachmentsService roomAttachmentsService) : ControllerBase
{
    /// <summary>
    /// Сохранить файл в хранилище
    /// </summary>
    /// <param name="id">Идентификатор файла (внутреннее название)</param>
    /// <param name="file">файл</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Модель сохраненного файла</returns>
    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<FileDescriptorModel>> StoreFile([FromQuery] Guid id, IFormFile file, CancellationToken cancellationToken)
    {
        var descriptor = await roomAttachmentsService.Store(
            id,
            file.FileName,
            content: file.OpenReadStream(),
            file.Length,
            cancellationToken);

        var response = new FileDescriptorModel(
            file.FileName,
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
    public async Task<ActionResult<TempFileUrlDto?>> GetAttachment([FromQuery] Guid id, [FromQuery] string bucket)
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