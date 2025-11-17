namespace Rooms.Domain.Models.File;

/// <summary>
/// Расположение файла
/// </summary>
public class FileLocation
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Бакет в хранилище
    /// </summary>
    public string Bucket { get; set; } = null!;
}