using Application.Implementations.Dtos.Room;

namespace Application.Implementations.Dtos.Responses;

public record RoomsResponseDto
{
    public RoomDto[] Rooms { get; init; } = [];
    public int Count { get; init; }
}
