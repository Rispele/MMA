using WebApi.Core.Models.RoomSchedule;

namespace WebApi.Core.Models.Responses;

public record RoomScheduleResponseModel
{
    public RoomScheduleModel[] ScheduleModels { get; init; } = [];
    public int Count { get; init; }
}