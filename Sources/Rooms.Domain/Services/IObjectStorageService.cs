using Rooms.Domain.Models.File;

namespace Rooms.Domain.Services;

public interface IObjectStorageService
{
    Task<FileDescriptor> StoreObject(
        Guid id,
        string bucket,
        string filename,
        Stream content,
        long length,
        CancellationToken cancellationToken = default);

    Task<TempFileUrl> FindObject(FileLocation location, int expiresIn);

    Task Remove(FileLocation location, CancellationToken cancellationToken = default);
}