using Rooms.Core.Dtos.Files;

namespace Rooms.Core.Services.Interfaces;

public interface IMinioStorageService
{
    Task<byte[]> GetDataAsync(Guid fileId);

    Task<FileLocationDto> StoreDataAsync(Stream content, CancellationToken cancellationToken);

    Task RemoveAsync(Guid fileId);
}