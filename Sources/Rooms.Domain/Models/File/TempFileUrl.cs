namespace Rooms.Domain.Models.File;

/// <summary>
/// Временная ссылка на файл
/// </summary>
public readonly struct TempFileUrl
{
    /// <summary>
    /// URL
    /// </summary>
    public required string Url { get; init; }
}