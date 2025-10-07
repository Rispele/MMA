namespace Rooms.Core.Implementations.Services.Files;

public class MinioOptions
{
    public int ExpireSeconds { get; set; }
    public string BucketName { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
    public string AccessKey { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public string Region { get; set; } = default!;
}