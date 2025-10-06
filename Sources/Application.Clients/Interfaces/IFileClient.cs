using Application.Clients.Dtos;
using Application.Clients.Dtos.Files;

namespace Application.Clients.Interfaces;

public interface IFileClient
{
    Task<FileDto> GetFileById(Guid fileId, CancellationToken cancellationToken = default);
    Task<FileLocationDto> StoreFile(byte[] content, CancellationToken cancellationToken = default);
    Task RemoveFileById(Guid fileId);
}