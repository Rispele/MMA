using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Domain.Models.Rooms.Parameters;

public class RoomParameters
{
    public RoomType Type { get; set; }
    public RoomLayout Layout { get; set; }
    public RoomNetType NetType { get; set; }
    public int? Seats { get; set; }
    public int? ComputerSeats { get; set; }
    public bool? HasConditioning { get; set; }
}