namespace Rooms.Core.Dtos.Files;

public record FileExportDto
{
    public required string FileName { get; init; }
    public required Stream Content { get; init; }
    public required string ContentType { get; init; }
}