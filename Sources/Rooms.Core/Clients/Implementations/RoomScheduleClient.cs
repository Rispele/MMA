using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Requests.RoomSchedule;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients.Implementations;

public class RoomScheduleClient : IRoomScheduleClient
{
    public Task<IEnumerable<RoomScheduleResponseDto>> GetRoomSchedule(GetRoomScheduleDto dto)
    {
        var response = (IEnumerable<RoomScheduleResponseDto>)new List<RoomScheduleResponseDto>() { new() };
        return Task.FromResult(response);
    }
}