namespace WebApi.Models.RoomSchedule;

public record RoomScheduleModel
{
    public required DateOnly Date { get; init; }
    public required TimeOnly From { get; init; }
    public required TimeOnly To { get; init; }
    public required string Title { get; init; }
    public required string Teacher { get; init; }
    public required string GroupTitle { get; init; }
}