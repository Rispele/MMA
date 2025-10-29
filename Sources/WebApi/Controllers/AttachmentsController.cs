using Microsoft.AspNetCore.Mvc;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Services.Interfaces;
using WebApi.Models.Files;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/attachments")]
public class AttachmentsController(IRoomAttachmentsService roomAttachmentsService) : ControllerBase
{
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
                descriptor.FileLocation.Id,
                descriptor.FileLocation.Bucket));

        return Ok(response);
    }

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