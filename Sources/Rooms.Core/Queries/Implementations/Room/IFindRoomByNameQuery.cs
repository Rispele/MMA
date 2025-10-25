using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Room;

public interface IFindRoomByNameQuery : ISingleQuerySpecification<Models.Room.Room>
{
    public string Name { get; init; }
}