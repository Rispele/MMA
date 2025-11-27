using Commons.Domain.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

internal sealed record FindRoomByIdQuery(int RoomId) : ISingleQuerySpecification<FindRoomByIdQuery, Domain.Models.Rooms.Room>;