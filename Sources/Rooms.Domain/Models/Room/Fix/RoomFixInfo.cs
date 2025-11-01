namespace Rooms.Domain.Models.Room.Fix;

public class RoomFixInfo
{
    public RoomStatus Status { get; set; }
    public DateTime? FixDeadline { get; set; }
    public string? Comment { get; set; }
}