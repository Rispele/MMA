namespace WebApi.Models.Dtos;

public class StorageFileDto
{
    public Guid Id { get; set; }
    public string BucketName { get; set; } = default!;
}