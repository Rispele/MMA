namespace Rooms.Domain.Models.File;

public class FileLocation
{
    public Guid Id { get; set; }
    public string Bucket { get; set; } = null!;
}