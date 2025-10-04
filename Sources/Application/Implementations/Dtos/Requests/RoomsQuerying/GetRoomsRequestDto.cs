namespace Application.Implementations.Dtos.Requests.RoomsQuerying;

public record GetRoomsRequestDto
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterRoomId { get; init; }
    public RoomsFilterDto? Filter { get; init; }
}
