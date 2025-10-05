using WebApi.Models.Dtos;

namespace WebApi.Services.Interfaces;

public interface IMinioStorageService
{
    Task<byte[]> GetDataAsync(Guid fileId);

    Task<StorageFileDto> StoreDataAsync(Stream content, CancellationToken cancellationToken);

    Task RemoveAsync(Guid fileId);
}