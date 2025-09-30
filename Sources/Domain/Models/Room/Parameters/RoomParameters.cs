namespace Domain.Models.Room.Parameters;

public class RoomParameters
{
     public RoomType Type { get; private set; }
     public RoomLayout Layout { get; private set; }
     public RoomNetType NetType { get; private set; }
     public int Seats { get; private set; }
     public int ComputerSeats { get; private set; }
     public bool HasConditioning { get; private set; }
}