using WebApi.Models.Dtos;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class FileService(IMinioStorageService minioStorageService) : IFileService
{
    public async Task<FileResultDto?> GetFileAsync(Guid id, CancellationToken cancellationToken)
    {
        var data = await minioStorageService.GetDataAsync(id);

        return new FileResultDto
        {
            Stream = new MemoryStream(data),
        };
    }

    public async Task<StorageFileDto> StoreFileAsync(Stream content, CancellationToken cancellationToken)
    {
        return await minioStorageService.StoreDataAsync(content, cancellationToken);
    }

    public async Task RemoveFileAsync(Guid fileId)
    {
        await minioStorageService.RemoveAsync(fileId);
    }
}