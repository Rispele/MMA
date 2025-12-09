using WebApi.Core.Models.Requests.RoomSchedule;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Interfaces;

public interface IRoomScheduleService
{
    Task<RoomScheduleResponseModel> GetRoomSchedule(GetRoomScheduleModel model, CancellationToken cancellationToken);
}