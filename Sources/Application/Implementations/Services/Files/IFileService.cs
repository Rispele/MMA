using Application.Implementations.Dtos.Files;

namespace Application.Implementations.Services.Files;

public interface IFileService
{
    /// <summary>
    /// Returns file stream, file name and content type (or null if not found)
    /// Implement via MinIO / S3 client.
    /// </summary>
    Task<FileResultDto?> GetFileAsync(Guid id, CancellationToken cancellationToken);

    Task<Guid> StoreFileAsync(Stream content, CancellationToken cancellationToken);

    Task RemoveFileAsync(Guid fileId);
}