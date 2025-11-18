using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.BookingRequests;

/// <summary>
/// Частота проведения мероприятия
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BookingFrequency
{
    /// <summary>
    /// Ежедневно
    /// </summary>
    Everyday = 1,

    /// <summary>
    /// Еженедельно
    /// </summary>
    Weekly = 2,
}