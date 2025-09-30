using WebApi.Dto.Room;

namespace WebApi.Dto.Responses;

public record RoomsResponse
{
    public RoomModel[] Rooms { get; init; } = Array.Empty<RoomModel>();
    public int Count { get; init; }
}
