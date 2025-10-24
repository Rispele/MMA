namespace Rooms.Core.Dtos.Requests.Rooms;

public record GetRoomsDto(int BatchNumber, int BatchSize, int AfterRoomId, RoomsFilterDto? Filter);