using System.ComponentModel;

namespace Rooms.Domain.Models.BookingRequests;

/// <summary>
/// Статус заявки в расписании
/// </summary>
public enum BookingScheduleStatus
{
    /// <summary>
    /// Не отправлено
    /// </summary>
    [Description("Не отправлено")]
    NotSent = 1,

    /// <summary>
    /// Забронировано
    /// </summary>
    [Description("Забронировано")]
    Booked = 2,

    /// <summary>
    /// Бронь подтверждена
    /// </summary>
    [Description("Бронь подтверждена")]
    BookingApproved = 3,

    /// <summary>
    /// Бронь снята
    /// </summary>
    [Description("Бронь снята")]
    BookingCancelled = 4,

    /// <summary>
    /// Ошибка снятия брони
    /// </summary>
    [Description("Ошибка снятия брони")]
    BookingCancelError = 5,
}