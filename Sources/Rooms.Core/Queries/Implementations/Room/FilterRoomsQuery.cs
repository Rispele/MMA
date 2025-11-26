using Rooms.Core.Dtos.Room.Requests;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public sealed record FilterRoomsQuery(int BatchSize, int BatchNumber, int AfterRoomId, RoomsFilterDto? Filter)
    : IQuerySpecification<FilterRoomsQuery, Domain.Models.Rooms.Room>;