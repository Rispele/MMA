namespace Rooms.Core.Dtos.Files;

public class FileDto
{
    public Stream Stream { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string? ContentType { get; set; }
}