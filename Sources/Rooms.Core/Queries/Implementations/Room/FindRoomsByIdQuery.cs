using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public sealed record FindRoomsByIdQuery(IEnumerable<int> RoomIds) : IQuerySpecification<FindRoomsByIdQuery, Domain.Models.Room.Room>;