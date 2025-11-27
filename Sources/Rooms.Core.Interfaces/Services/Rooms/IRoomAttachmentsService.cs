using Rooms.Core.Interfaces.Dtos.Files;

namespace Rooms.Core.Interfaces.Services.Rooms;

public interface IRoomAttachmentsService
{
    public Task<TempFileUrlDto?> Load(FileLocationDto fileLocation);

    public Task<FileDescriptorDto> Store(
        Guid id,
        string fileName,
        Stream content,
        long length,
        CancellationToken cancellationToken);

    public Task Remove(FileLocationDto fileLocation, CancellationToken cancellationToken);
}