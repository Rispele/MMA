using JetBrains.Annotations;

namespace Domain.Models;

public class File
{
    public string Filename { get; init; } = null!;
    public Location Location { get; init; } = null!;

    [UsedImplicitly]
    protected File()
    {
    }
    
    public File(string filename, Location location)
    {
        Filename = filename;
        Location = location;
    }
}