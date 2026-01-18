using Rooms.Domain.Propagated.Rooms;

namespace WebApi.Core.Models.Room.Parameters;

public record RoomParametersModel
{
    public required RoomType Type { get; init; }
    public required RoomLayout Layout { get; init; }
    public required RoomNetType NetType { get; init; }
    public int? Seats { get; init; }
    public int? ComputerSeats { get; init; }
    public bool? HasConditioning { get; init; }
}