using JetBrains.Annotations;

namespace Rooms.Domain.Models.File;

public class FileDescriptor
{
    [UsedImplicitly]
    protected FileDescriptor()
    {
    }

    public FileDescriptor(string filename, FileLocation fileLocation)
    {
        Filename = filename;
        FileLocation = fileLocation;
    }

    public string Filename { get; init; } = null!;
    public FileLocation FileLocation { get; init; } = null!;
}