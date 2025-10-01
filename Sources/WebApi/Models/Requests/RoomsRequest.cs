namespace WebApi.Models.Requests;

public record RoomsRequest
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int AfterRoomId { get; init; }
    public RoomsFilter? Filter { get; init; }
}
