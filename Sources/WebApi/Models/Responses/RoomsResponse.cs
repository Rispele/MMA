using WebApi.Models.Room;

namespace WebApi.Models.Responses;

public record RoomsResponse
{
    public RoomModel[] Rooms { get; init; } = [];
    public int Count { get; init; }
}
