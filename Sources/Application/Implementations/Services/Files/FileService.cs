using Application.Implementations.Dtos.Files;

namespace Application.Implementations.Services.Files;

public class FileService : IFileService
{
    public Task<FileResultDto?> GetFileAsync(Guid id, string bucket, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}