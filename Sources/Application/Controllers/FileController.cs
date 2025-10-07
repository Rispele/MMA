using Application.Implementations.Dtos.Files;
using Application.Implementations.Services.Files;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("files")]
public class FileController(ILogger<RoomsController> logger, IFileService fileService) : ControllerBase
{
    [HttpGet("{fileId:guid}")]
    public async Task<FileDto?> GetFileById(Guid fileId, CancellationToken cancellationToken)
    {
        // todo: валидация доступа
        return await fileService.GetFileAsync(fileId, cancellationToken);
    }

    [HttpPost]
    public async Task<FileLocationDto> StoreFile([FromBody] byte[] content, CancellationToken cancellationToken)
    {
        return await fileService.StoreFileAsync(new MemoryStream(content), cancellationToken);
    }

    [HttpDelete("{fileId:guid}")]
    public async Task RemoveFileById(Guid fileId)
    {
        await fileService.RemoveFileAsync(fileId);
        // todo: полное или мягкое удаление записи о файле из базы (room, ...) - возможно, в транзакции
    }
}