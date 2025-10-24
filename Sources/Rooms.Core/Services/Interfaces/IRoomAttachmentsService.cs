using Rooms.Core.Dtos.Files;

namespace Rooms.Core.Services.Interfaces;

public interface IRoomAttachmentsService
{
    public Task<TempFileUrlDto?> GetFileAsync(FileLocationDto fileLocation);

    public Task<FileDescriptorDto> StoreFileAsync(
        Guid id,
        string fileName,
        Stream content,
        CancellationToken cancellationToken);

    public Task RemoveFileAsync(FileLocationDto fileLocation, CancellationToken cancellationToken);
}