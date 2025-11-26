namespace Rooms.Core.Clients.RoomSchedule;

public interface IRoomScheduleClient
{
    IAsyncEnumerable<RoomScheduleResponseItemDto?> GetRoomSchedule(GetRoomScheduleRequest request, CancellationToken cancellationToken);
}