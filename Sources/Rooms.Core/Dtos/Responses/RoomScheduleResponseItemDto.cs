using System.Text.Json.Serialization;

namespace Rooms.Core.Dtos.Responses;

public record RoomScheduleResponseItemDto
{
    [JsonPropertyName("date")]
    public required DateOnly Date { get; init; }
    
    [JsonPropertyName("timeBegin")]
    public required TimeOnly From  { get; init; }
    
    [JsonPropertyName("timeEnd")]
    public required TimeOnly To { get; init; }
    
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    
    [JsonPropertyName("teacherName")]
    public required string Teacher { get; init; }
    
    [JsonPropertyName("groupTitle")]
    public required string GroupTitle { get; init; }
}
