namespace Commons.ExternalClients.RoomSchedule;

public interface IRoomScheduleClient
{
    IAsyncEnumerable<RoomScheduleResponseItemDto?> GetRoomSchedule(GetRoomScheduleRequest request, CancellationToken cancellationToken);
}