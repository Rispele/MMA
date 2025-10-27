using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public interface IFilterRoomsQuery : IQuerySpecification<Domain.Models.Room.Room>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterRoomId { get; init; }
    public RoomsFilterDto? Filter { get; init; }
}