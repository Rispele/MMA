using JetBrains.Annotations;

namespace Domain.Models;

public class FileDescriptor
{
    public string Filename { get; init; } = null!;
    public Location Location { get; init; } = null!;

    [UsedImplicitly]
    protected FileDescriptor()
    {
    }
    
    public FileDescriptor(string filename, Location location)
    {
        Filename = filename;
        Location = location;
    }
}