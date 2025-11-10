using Rooms.Core.Dtos.Requests.RoomSchedule;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.Interfaces;

public interface IRoomScheduleService
{
    Task<IEnumerable<RoomScheduleResponseDto>> GetRoomScheduleAsync(GetRoomScheduleDto dto);
}