using WebApi.Models.Dtos;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class FileService : IFileService
{
    public Task<FileResultDto?> GetFileAsync(Guid id, string bucket, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}