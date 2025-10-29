namespace Rooms.Core.Dtos.Requests.Rooms;

public record GetRoomsRequestDto
{
    public required int BatchNumber { get; init; }
    public required int BatchSize { get; init; }
    public required int AfterRoomId { get; init; }
    public required RoomsFilterDto? Filter { get; init; }
}