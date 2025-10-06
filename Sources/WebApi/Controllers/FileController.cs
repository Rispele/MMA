using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Files;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("files")]
public class FileController(IFileService fileService) : ControllerBase
{
    [HttpPost]
    public async Task<FileLocationModel> StoreFile([FromBody] byte[] content, CancellationToken cancellationToken)
    {
        return await fileService.StoreFileAsync(new MemoryStream(content), cancellationToken);
    }
}