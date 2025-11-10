using WebApi.Models.RoomSchedule;

namespace WebApi.Models.Responses;

public record RoomScheduleResponseModel
{
    public RoomScheduleModel[] ScheduleModels { get; init; } = [];
    public int Count { get; init; }
}