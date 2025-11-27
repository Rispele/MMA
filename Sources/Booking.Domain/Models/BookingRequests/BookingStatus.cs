using System.ComponentModel;

namespace Booking.Domain.Models.BookingRequests;

public enum BookingStatus
{
    [Description("Новая")]
    New = 1,

    [Description("На согласовании")]
    InApprove = 2,

    [Description("На модерации")]
    UnderModeration = 3,

    [Description("Отклонена")]
    Rejected = 4,

    [Description("Согласование в СЭД отклонено")]
    SedRejected = 5,

    [Description("Подтверждена")]
    Approved = 6,

    [Description("Мероприятие завершено")]
    EventFinished = 7,

    [Description("Ошибка")]
    Error = 8,
}