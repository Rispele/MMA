using Rooms.Core.Clients.RoomSchedule;
using Rooms.Core.Dtos.Room.Requests;

namespace Rooms.Core.Services.Rooms.Interfaces;

public interface IScheduleService
{
    IAsyncEnumerable<RoomScheduleResponseItemDto> GetRoomSchedule(GetRoomScheduleDto dto, CancellationToken cancellationToken);
}