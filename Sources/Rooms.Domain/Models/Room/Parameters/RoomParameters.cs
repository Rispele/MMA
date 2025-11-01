namespace Rooms.Domain.Models.Room.Parameters;

public class RoomParameters
{
    public RoomType Type { get; set; }
    public RoomLayout Layout { get; set; }
    public RoomNetType NetType { get; set; }
    public int? Seats { get; set; }
    public int? ComputerSeats { get; set; }
    public bool? HasConditioning { get; set; }
}