using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Room;

public interface IFilterRoomsQuery : IQuerySpecification<Models.Room.Room>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterRoomId { get; init; }
    public RoomsFilterDto? Filter { get; init; }
}