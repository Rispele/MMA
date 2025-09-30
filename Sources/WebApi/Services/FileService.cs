namespace WebApi.Services;

public class FileService : IFileService
{
    public Task<FileResultModel?> GetFileAsync(Guid id, string bucket, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}