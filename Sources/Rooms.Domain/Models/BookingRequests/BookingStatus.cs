using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.BookingRequests;

/// <summary>
/// Статус заявки
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BookingStatus
{
    /// <summary>
    /// Новая
    /// </summary>
    [Description("Новая")]
    New = 1,

    /// <summary>
    /// На согласовании
    /// </summary>
    [Description("На согласовании")]
    InApprove = 2,

    /// <summary>
    /// На модерации
    /// </summary>
    [Description("На модерации")]
    UnderModeration = 3,

    /// <summary>
    /// Отклонена
    /// </summary>
    [Description("Отклонена")]
    Rejected = 4,

    /// <summary>
    /// Согласование в СЭД отклонено
    /// </summary>
    [Description("Согласование в СЭД отклонено")]
    SedRejected = 5,

    /// <summary>
    /// Подтверждена
    /// </summary>
    [Description("Подтверждена")]
    Approved = 6,

    /// <summary>
    /// Мероприятие завершено
    /// </summary>
    [Description("Мероприятие завершено")]
    EventFinished = 7,

    /// <summary>
    /// Ошибка
    /// </summary>
    [Description("Ошибка")]
    Error = 8,
}