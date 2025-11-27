using Commons.Domain.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public sealed record FindRoomByIdQuery(int RoomId) : ISingleQuerySpecification<FindRoomByIdQuery, Domain.Models.Rooms.Room>;