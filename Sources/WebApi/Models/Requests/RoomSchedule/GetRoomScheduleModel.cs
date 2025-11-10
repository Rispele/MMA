namespace WebApi.Models.Requests.RoomSchedule;

public class GetRoomScheduleModel
{
    public string Room { get; set; } = null!;
    public DateOnly Date { get; set; }
}