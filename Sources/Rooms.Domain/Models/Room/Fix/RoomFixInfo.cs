namespace Rooms.Domain.Models.Room.Fix;

/// <summary>
/// Статусная модель аудитории
/// </summary>
public class RoomFixInfo
{
    /// <summary>
    /// Статус аудитории
    /// </summary>
    public RoomStatus Status { get; set; }

    /// <summary>
    /// Крайний срок исправления аудитории
    /// </summary>
    public DateTime? FixDeadline { get; set; }

    /// <summary>
    /// Комментарий
    /// </summary>
    public string? Comment { get; set; }
}