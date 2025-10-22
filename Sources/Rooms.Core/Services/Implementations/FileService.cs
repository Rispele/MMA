using Rooms.Core.Dtos.Files;
using Rooms.Core.Services.Interfaces;

namespace Rooms.Core.Services.Implementations;

public class FileService(IMinioStorageService minioStorageService) : IFileService
{
    public async Task<FileDto?> GetFileAsync(Guid id, CancellationToken cancellationToken)
    {
        var data = await minioStorageService.GetDataAsync(id);

        return new FileDto
        {
            Stream = new MemoryStream(data),
        };
    }

    public async Task<FileLocationDto> StoreFileAsync(Stream content, CancellationToken cancellationToken)
    {
        return await minioStorageService.StoreDataAsync(content, cancellationToken);
    }

    public async Task RemoveFileAsync(Guid fileId)
    {
        await minioStorageService.RemoveAsync(fileId);
    }
}