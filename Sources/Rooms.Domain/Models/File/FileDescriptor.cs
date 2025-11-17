namespace Rooms.Domain.Models.File;

/// <summary>
/// Данные файла
/// </summary>
public class FileDescriptor
{
    /// <summary>
    /// Название файла
    /// </summary>
    public string Filename { get; init; } = null!;

    /// <summary>
    /// Расположение файла
    /// </summary>
    public FileLocation Location { get; init; } = null!;
}