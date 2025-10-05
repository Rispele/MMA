using Application.Implementations.Dtos.Files;
using Application.Implementations.Services.Files;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("files")]
public class FileController(ILogger<RoomsController> logger, IFileService fileService) : ControllerBase
{
    [HttpGet("{fileId:guid}")]
    public async Task<ActionResult<FileResultDto>> GetFileById(Guid fileId, CancellationToken cancellationToken)
    {
        // todo: валидация доступа
        var file = await fileService.GetFileAsync(fileId, cancellationToken);
        if (file == null)
        {
            return NotFound();
        }

        return File(file.Stream, file.ContentType!, file.FileName);
    }

    [HttpPost("{fileName}")]
    public async Task<ActionResult<Guid>> StoreFile([FromBody] byte[] content, string fileName, CancellationToken cancellationToken)
    {
        var fileId = await fileService.StoreFileAsync(new MemoryStream(content), cancellationToken);
        // todo: сохранение записи о файле в базу (имя, id в хранилище)

        return Ok(fileId);
    }

    [HttpDelete("{fileId:guid}")]
    public async Task<ActionResult<Guid>> RemoveFileById(Guid fileId)
    {
        await fileService.RemoveFileAsync(fileId);
        // todo: полное или мягкое удаление записи о файле из базы (имя, id в хранилище)

        return Ok();
    }
}