using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public sealed record FindRoomByIdQuery(int RoomId) : ISingleQuerySpecification<FindRoomByIdQuery, Domain.Models.Room.Room>;