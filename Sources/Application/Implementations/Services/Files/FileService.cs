using Application.Implementations.Dtos.Dtos;

namespace Application.Implementations.Services.Attachments;

public class FileService : IFileService
{
    public Task<FileResultDto?> GetFileAsync(Guid id, string bucket, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}