namespace Rooms.Core.Dtos;

public class FileExportDto
{
    public required string FileName { get; set; }
    public required Stream Content { get; set; }
    public required string ContentType { get; set; }
}