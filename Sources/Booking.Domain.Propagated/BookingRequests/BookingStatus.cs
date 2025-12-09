using System.ComponentModel;

namespace Booking.Domain.Propagated.BookingRequests;

public enum BookingStatus
{
    [Description("Новая")]
    New = 1,

    [Description("На согласовании")]
    SentForApprovalInEdms = 2,

    [Description("Согласование в СЭД отклонено")]
    RejectedInEdms = 3,

    [Description("На модерации")]
    SentForModeration = 4,

    [Description("Отклонена")]
    RejectedByModerator = 5,

    [Description("Подтверждена")]
    ApprovedByModerator = 6,

    [Description("Мероприятие завершено")]
    EventFinished = 7,

    [Description("Ошибка")]
    Error = 8
}