namespace WebApi.Models.Requests.RoomSchedule;

public class GetRoomScheduleModel
{
    public int RoomId { get; set; }
    public DateOnly Date { get; set; }
}