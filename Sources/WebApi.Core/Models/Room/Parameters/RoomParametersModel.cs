namespace WebApi.Core.Models.Room.Parameters;

public record RoomParametersModel
{
    public required RoomTypeModel Type { get; init; }
    public required RoomLayoutModel Layout { get; init; }
    public required RoomNetTypeModel NetType { get; init; }
    public int? Seats { get; init; }
    public int? ComputerSeats { get; init; }
    public bool? HasConditioning { get; init; }
}