namespace WebApi.Services;

public interface IFileService
{
    /// <summary>
    /// Returns file stream, file name and content type (or null if not found)
    /// Implement via MinIO / S3 client.
    /// </summary>
    Task<FileResultModel?> GetFileAsync(Guid id, string bucket, CancellationToken cancellationToken);
}