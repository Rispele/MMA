using System.Text.Json.Serialization;

namespace Commons.Core.Models.Filtering;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirectionDto
{
    None = 0,
    Ascending = 1,
    Descending = 2
}