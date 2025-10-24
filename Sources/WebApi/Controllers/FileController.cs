using Microsoft.AspNetCore.Mvc;
using Rooms.Core.Services.Interfaces;
using WebApi.Models.Files;

namespace WebApi.Controllers;

[ApiController]
[Route("files")]
public class FileController(IRoomAttachmentsService roomAttachmentsService) : ControllerBase
{
    [HttpPost]
    public async Task<FileLocationModel> StoreFile(
        [FromQuery] Guid id,
        [FromQuery] string fileName,
        [FromBody] byte[] content,
        CancellationToken cancellationToken)
    {
        var descriptor = await roomAttachmentsService.StoreFileAsync(
            id,
            fileName,
            new MemoryStream(content),
            cancellationToken);

        return new FileLocationModel(descriptor.FileLocation.Id, descriptor.FileLocation.Bucket);
    }
}