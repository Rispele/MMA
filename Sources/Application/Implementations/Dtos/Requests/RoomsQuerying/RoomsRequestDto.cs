namespace Application.Implementations.Dtos.Requests.RoomsQuerying;

public record RoomsRequestDto
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int AfterRoomId { get; init; }
    public RoomsFilter? Filter { get; init; }
}
