using Rooms.Core.Dtos.Files;

namespace Rooms.Core.Services.Interfaces;

public interface IRoomAttachmentsService
{
    public Task<TempFileUrlDto?> Load(FileLocationDto fileLocation);

    public Task<FileDescriptorDto> Store(
        Guid id,
        string fileName,
        Stream content,
        CancellationToken cancellationToken);

    public Task Remove(FileLocationDto fileLocation, CancellationToken cancellationToken);
}