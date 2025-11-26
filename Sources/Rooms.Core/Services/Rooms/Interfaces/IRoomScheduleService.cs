using Rooms.Core.Dtos.Requests.RoomSchedule;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.Rooms.Interfaces;

public interface IRoomScheduleService
{
    Task<IEnumerable<RoomScheduleResponseDto>> GetRoomScheduleAsync(GetRoomScheduleDto dto, CancellationToken cancellationToken);
}