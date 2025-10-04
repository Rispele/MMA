namespace Application.Implementations.Dtos.Files;

public class FileResultDto
{
    public Stream Stream { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string? ContentType { get; set; }
}