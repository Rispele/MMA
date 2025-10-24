using Commons;
using Minio;
using Minio.DataModel.Args;
using Rooms.Domain.Models.File;
using Rooms.Domain.Services;

namespace Rooms.Persistence.ObjectStorageService;

public class MinioObjectStorageService(IMinioClient minioClient) : IObjectStorageService
{
    public async Task<FileDescriptor> StoreObject(
        Guid id,
        string bucket,
        string filename,
        Stream content,
        long length,
        CancellationToken cancellationToken = default)
    {
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucket)
            .WithObject(id.ToString())
            .WithStreamData(content)
            .WithObjectSize(length);

        await minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

        return new FileDescriptor(filename, new FileLocation(id, bucket));
    }

    public async Task<TempFileUrl> FindObject(FileLocation location, int expiresIn)
    {
        var contentArgs = new PresignedGetObjectArgs()
            .WithBucket(location.Bucket)
            .WithObject(location.Id.ToString())
            .WithExpiry(expiresIn);

        var downloadLink = await minioClient.PresignedGetObjectAsync(contentArgs);

        return downloadLink.Map(url => new TempFileUrl { Url = url });
    }

    public Task Remove(FileLocation location, CancellationToken cancellationToken = default)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(location.Bucket)
            .WithObject(location.Id.ToString());

        return minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
    }
}