using Rooms.Core.Dtos.Requests.RoomSchedule;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients.Interfaces;

public interface IRoomScheduleClient
{
    IAsyncEnumerable<RoomScheduleResponseItemDto?> GetRoomSchedule(GetRoomScheduleRequest request, CancellationToken cancellationToken);
}