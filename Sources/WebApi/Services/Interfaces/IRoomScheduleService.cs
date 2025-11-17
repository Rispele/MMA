using WebApi.Models.Requests.RoomSchedule;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IRoomScheduleService
{
    Task<RoomScheduleResponseModel> GetRoomSchedule(GetRoomScheduleModel model, CancellationToken cancellationToken);
}