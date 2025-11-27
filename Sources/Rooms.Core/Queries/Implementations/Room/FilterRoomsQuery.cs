using Commons.Domain.Queries.Abstractions;
using Rooms.Core.Interfaces.Dtos.Room.Requests;

namespace Rooms.Core.Queries.Implementations.Room;

internal sealed record FilterRoomsQuery(int BatchSize, int BatchNumber, int AfterRoomId, RoomsFilterDto? Filter)
    : IQuerySpecification<FilterRoomsQuery, Domain.Models.Rooms.Room>;