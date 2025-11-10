using Rooms.Core.Dtos.Requests.RoomSchedule;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients.Interfaces;

public interface IRoomScheduleClient
{
    Task<IEnumerable<RoomScheduleResponseDto>> GetRoomSchedule(GetRoomScheduleDto dto);
}