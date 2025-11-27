using Commons.Domain.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

internal sealed record FindRoomByNameQuery(string Name) : ISingleQuerySpecification<FindRoomByNameQuery, Domain.Models.Rooms.Room>;