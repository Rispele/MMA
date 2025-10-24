using JetBrains.Annotations;

namespace Rooms.Domain.Models.File;

public class FileLocation
{
    [UsedImplicitly]
    protected FileLocation()
    {
    }

    public FileLocation(Guid id, string bucket)
    {
        Id = id;
        Bucket = bucket;
    }

    public Guid Id { get; private set; }
    public string Bucket { get; private set; } = null!;
}