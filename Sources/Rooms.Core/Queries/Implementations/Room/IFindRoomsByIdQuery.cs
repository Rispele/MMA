using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public interface IFindRoomsByIdQuery : IQuerySpecification<Domain.Models.Room.Room>
{
    public IEnumerable<int> RoomIds { get; init; }
}