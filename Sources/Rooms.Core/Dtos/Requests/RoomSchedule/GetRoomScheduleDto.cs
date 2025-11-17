namespace Rooms.Core.Dtos.Requests.RoomSchedule;

public class GetRoomScheduleDto
{
    public int RoomId { get; set; }
    public DateOnly Date { get; set; }
}