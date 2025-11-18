using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.BookingRequests;

/// <summary>
/// Тип мероприятия
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
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