using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using WebApi.Models.Dtos;
using WebApi.Options;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class MinioStorageService : IMinioStorageService
{
    private readonly MinioOptions _minioOptions;
    private readonly IMinioClient _minioClient;

    public MinioStorageService(IOptions<MinioOptions> minioOptions)
    {
        _minioOptions = minioOptions.Value;

        _minioClient = new MinioClient()
            .WithEndpoint(_minioOptions.Endpoint)
            .WithCredentials(_minioOptions.AccessKey, _minioOptions.SecretKey)
            .WithRegion(_minioOptions.Region)
            .Build();
    }

    public async Task<byte[]> GetDataAsync(Guid fileId)
    {
        var contentArgs = new PresignedGetObjectArgs()
            .WithBucket(_minioOptions.BucketName)
            .WithObject(fileId.ToString())
            .WithExpiry(_minioOptions.ExpireSeconds);

        var downloadLink = await _minioClient.PresignedGetObjectAsync(contentArgs);
        var stream = await new HttpClient().GetStreamAsync(downloadLink);

        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async Task<StorageFileDto> StoreDataAsync(Stream content, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_minioOptions.BucketName)
            .WithObject(id.ToString())
            .WithStreamData(content);
        await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
        return new StorageFileDto
        {
            Id = id,
            BucketName = _minioOptions.BucketName,
        };
    }

    public async Task RemoveAsync(Guid fileId)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(_minioOptions.BucketName)
            .WithObject(fileId.ToString());

        await _minioClient.RemoveObjectAsync(removeObjectArgs);
    }
}