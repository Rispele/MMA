using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Clients.Dtos.Requests.Filtering;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirectionDto
{
    None = 0,
    Ascending = 1,
    Descending = 2
}