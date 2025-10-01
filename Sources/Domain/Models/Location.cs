using JetBrains.Annotations;

namespace Domain.Models;

public class Location
{
    public Guid Id { get; private set; }
    public string Bucket { get; private set; } = null!;

    [UsedImplicitly]
    protected Location()
    {
    }
    
    public Location(Guid id, string bucket)
    {
        Id = id;
        Bucket = bucket;
    }
}