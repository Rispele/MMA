namespace Rooms.Domain.Models.Room.Parameters;

public class RoomParameters
{
    // (d.smirnov): крч оно как-то говённо иногда работает, решил пока что вырубить
    // ReSharper disable once ConvertToPrimaryConstructor
    public RoomParameters(
        RoomType type,
        RoomLayout layout,
        RoomNetType netType,
        int? seats,
        int? computerSeats,
        bool? hasConditioning)
    {
        Type = type;
        Layout = layout;
        NetType = netType;
        Seats = seats;
        ComputerSeats = computerSeats;
        HasConditioning = hasConditioning;
    }

    public RoomType Type { get; private set; }
    public RoomLayout Layout { get; private set; }
    public RoomNetType NetType { get; private set; }
    public int? Seats { get; private set; }
    public int? ComputerSeats { get; private set; }
    public bool? HasConditioning { get; private set; }
}