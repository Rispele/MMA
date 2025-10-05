using Application.Implementations.Dtos.Files;

namespace Application.Implementations.Services.Files;

public interface IMinioStorageService
{
    Task<byte[]> GetDataAsync(Guid fileId);

    Task<FileLocationDto> StoreDataAsync(Stream content, CancellationToken cancellationToken);

    Task RemoveAsync(Guid fileId);
}