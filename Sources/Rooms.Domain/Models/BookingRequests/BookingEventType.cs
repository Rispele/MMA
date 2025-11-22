using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.BookingRequests;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BookingEventType
{
    [Description("Научное")]
    Scientific = 1,

    [Description("Студенческое")]
    Student = 2,

    [Description("Прочее")]
    Other = 3,
}