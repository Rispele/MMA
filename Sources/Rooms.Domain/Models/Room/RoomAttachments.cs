using Rooms.Domain.Models.File;

namespace Rooms.Domain.Models.Room;

/// <summary>
/// Приложения к аудитории
/// </summary>
public class RoomAttachments
{
    /// <summary>
    /// Схема аудитории в формате PDF
    /// </summary>
    public FileDescriptor? PdfRoomScheme { get; set; }

    /// <summary>
    /// Изображение аудитории
    /// </summary>
    public FileDescriptor? Photo { get; set; }
}