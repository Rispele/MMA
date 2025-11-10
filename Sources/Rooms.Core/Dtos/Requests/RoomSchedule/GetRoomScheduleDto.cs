namespace Rooms.Core.Dtos.Requests.RoomSchedule;

public class GetRoomScheduleDto
{
    public string Room { get; set; } = null!;
    public DateOnly Date { get; set; }
}