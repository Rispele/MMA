using JetBrains.Annotations;

namespace Rooms.Domain.Models;

public class FileDescriptor
{
    public string Filename { get; init; } = null!;
    public FileLocation FileLocation { get; init; } = null!;

    [UsedImplicitly]
    protected FileDescriptor()
    {
    }

    public FileDescriptor(string filename, FileLocation fileLocation)
    {
        Filename = filename;
        FileLocation = fileLocation;
    }
}