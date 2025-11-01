namespace Rooms.Domain.Models.File;

public class FileDescriptor
{
    public string Filename { get; init; } = null!;
    public FileLocation Location { get; init; } = null!;
}