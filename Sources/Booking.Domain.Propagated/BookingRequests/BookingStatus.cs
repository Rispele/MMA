using System.ComponentModel;

namespace Booking.Domain.Propagated.BookingRequests;

public enum BookingStatus
{
    [Description("Новая")]
    New = 1,
    
    [Description("Инициирован процесс бронирования")]
    Initiated = 10,

    [Description("На согласовании")]
    SentForApprovalInEdms = 2,

    [Description("Согласование в СЭД отклонено")]
    RejectedInEdms = 3,
    
    [Description("Согласование в СЭД отклонено")]
    ApprovedInEdms = 4,

    [Description("На модерации")]
    SentForModeration = 5,

    [Description("Отклонена")]
    RejectedByModerator = 6,

    [Description("Подтверждена")]
    ApprovedByModerator = 7,

    [Description("Мероприятие завершено")]
    EventFinished = 8,

    [Description("Ошибка")]
    Error = 9
}