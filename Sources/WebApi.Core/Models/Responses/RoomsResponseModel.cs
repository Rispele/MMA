using WebApi.Core.Models.Room;

namespace WebApi.Core.Models.Responses;

public record RoomsResponseModel
{
    public RoomModel[] Rooms { get; init; } = [];
    public int Count { get; init; }
    public int? LastRoomId { get; init; }
}