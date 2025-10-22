using Microsoft.AspNetCore.Mvc;
using Rooms.Core.Services.Interfaces;
using WebApi.Models.Files;

namespace WebApi.Controllers;

[ApiController]
[Route("files")]
public class FileController(IFileService fileService) : ControllerBase
{
    [HttpPost]
    public async Task<FileLocationModel> StoreFile([FromBody] byte[] content, CancellationToken cancellationToken)
    {
        var location = await fileService.StoreFileAsync(new MemoryStream(content), cancellationToken);

        return new FileLocationModel(location.Id, location.Bucket);
    }
}