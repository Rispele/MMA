using Rooms.Core.Dtos.Requests.RoomSchedule;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.Rooms.Interfaces;

public interface IRoomScheduleService
{
    IAsyncEnumerable<RoomScheduleResponseItemDto> GetRoomSchedule(GetRoomScheduleDto dto, CancellationToken cancellationToken);
}