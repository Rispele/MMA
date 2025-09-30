namespace Domain.Models.Room.Fix;

public class RoomFixInfo
{
    public RoomStatus Status { get; private set; }
    public DateTime? FixDeadline { get; private set; }
    public string Comment { get; private set; }
    
    public RoomFixInfo(RoomStatus status, DateTime? fixDeadline, string comment)
    {
        Status = status;
        FixDeadline = fixDeadline;
        Comment = comment;
    }
}