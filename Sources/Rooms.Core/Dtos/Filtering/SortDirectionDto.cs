using System.Text.Json.Serialization;

namespace Rooms.Core.Dtos.Filtering;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirectionDto
{
    None = 0,
    Ascending = 1,
    Descending = 2
}