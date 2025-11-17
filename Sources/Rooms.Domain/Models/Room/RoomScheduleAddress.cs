namespace Rooms.Domain.Models.Room;

/// <summary>
/// Расположение аудитории
/// </summary>
public record RoomScheduleAddress
{
    /// <summary>
    /// Номер аудитории
    /// </summary>
    public string RoomNumber { get; init; } = null!;

    /// <summary>
    /// Адрес аудитории
    /// </summary>
    public string Address { get; init; } = null!;
}