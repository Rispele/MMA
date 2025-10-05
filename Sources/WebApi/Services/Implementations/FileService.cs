using WebApi.Models.Files;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class FileService : IFileService
{
    public Task<FileModel?> GetFileAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FileLocationModel> StoreFileAsync(Stream content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFileAsync(Guid fileId)
    {
        throw new NotImplementedException();
    }
}