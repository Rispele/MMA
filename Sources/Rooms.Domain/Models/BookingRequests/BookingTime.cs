namespace Rooms.Domain.Models.BookingRequests;

/// <summary>
/// Модель периода бронирования
/// </summary>
public class BookingTime
{
    /// <summary>
    /// Идентификатор аудитории
    /// </summary>
    public string RoomId { get; set; } = null!;

    /// <summary>
    /// Дата бронирования
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Начальное время бронирования
    /// </summary>
    public TimeOnly TimeFrom { get; set; }

    /// <summary>
    /// Конечное время бронирования
    /// </summary>
    public TimeOnly TimeTo { get; set; }

    /// <summary>
    /// Периодичность бронирования
    /// </summary>
    public BookingFrequency Frequency { get; set; }

    /// <summary>
    /// Конечная дата периодического бронирования
    /// </summary>
    public DateOnly? BookingFinishDate { get; set; }
}