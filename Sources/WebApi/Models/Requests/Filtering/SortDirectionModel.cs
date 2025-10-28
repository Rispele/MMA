using System.Text.Json.Serialization;

namespace WebApi.Models.Requests.Filtering;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirectionModel
{
    None = 0,
    Ascending = 1,
    Descending = 2
}