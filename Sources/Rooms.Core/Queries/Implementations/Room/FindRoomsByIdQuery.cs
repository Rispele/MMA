using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public sealed record FindRoomsByIdQuery(int[] RoomIds) : IQuerySpecification<FindRoomsByIdQuery, Domain.Models.Rooms.Room>;