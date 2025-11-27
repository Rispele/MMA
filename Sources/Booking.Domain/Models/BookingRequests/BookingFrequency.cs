using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.BookingRequests;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BookingFrequency
{
    Undefined = 0,

    [Description("Ежедневно")]
    Everyday = 1,

    [Description("Еженедельно")]
    Weekly = 2,
}