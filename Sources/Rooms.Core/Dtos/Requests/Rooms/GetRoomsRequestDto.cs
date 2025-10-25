namespace Rooms.Core.Dtos.Requests.Rooms;

public record GetRoomsRequestDto(int BatchNumber, int BatchSize, int AfterRoomId, RoomsFilterDto? Filter);