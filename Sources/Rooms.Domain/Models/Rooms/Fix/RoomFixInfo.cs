using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Domain.Models.Rooms.Fix;

public class RoomFixInfo
{
    public RoomStatus Status { get; set; }
    public DateTime? FixDeadline { get; set; }
    public string? Comment { get; set; }
}