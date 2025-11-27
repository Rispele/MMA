using System.ComponentModel;

namespace Rooms.Domain.Models.BookingRequests;

public enum BookingScheduleStatus
{
    [Description("Не отправлено")]
    NotSent = 1,

    [Description("Забронировано")]
    Booked = 2,

    [Description("Бронь подтверждена")]
    BookingApproved = 3,

    [Description("Бронь снята")]
    BookingCancelled = 4,

    [Description("Ошибка снятия брони")]
    BookingCancelError = 5,
}