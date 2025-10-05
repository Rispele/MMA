namespace WebApi.Models.Files;

public class FileModel
{
    public Stream Stream { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string? ContentType { get; set; }
}