using Commons.Domain.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

internal sealed record FindRoomsByIdQuery(int[] RoomIds) : IQuerySpecification<FindRoomsByIdQuery, Domain.Models.Rooms.Room>;