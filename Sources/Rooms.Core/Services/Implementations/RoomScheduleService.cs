using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Requests.RoomSchedule;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Services.Interfaces;

namespace Rooms.Core.Services.Implementations;

public class RoomScheduleService(IRoomScheduleClient roomScheduleClient) : IRoomScheduleService
{
    public async Task<IEnumerable<RoomScheduleResponseDto>> GetRoomScheduleAsync(GetRoomScheduleDto dto)
    {
        return await roomScheduleClient.GetRoomSchedule(dto);
    }
}