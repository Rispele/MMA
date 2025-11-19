namespace Rooms.Core.Dtos.Files;

public class FileExportDto
{
    public required string FileName { get; set; }
    public required MemoryStream Content { get; set; }
    public required string ContentType { get; set; }
}