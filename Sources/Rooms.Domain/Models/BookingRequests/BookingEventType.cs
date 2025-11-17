namespace Rooms.Domain.Models.BookingRequests;

/// <summary>
/// Тип мероприятия
/// </summary>
public enum BookingEventType
{
    /// <summary>
    /// Научное
    /// </summary>
    Scientific = 1,

    /// <summary>
    /// Студенческое
    /// </summary>
    Student = 2,

    /// <summary>
    /// Прочее
    /// </summary>
    Other = 3,
}