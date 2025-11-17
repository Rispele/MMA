using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public sealed record FindRoomByNameQuery(string Name) : ISingleQuerySpecification<FindRoomByNameQuery, Domain.Models.Room.Room>;