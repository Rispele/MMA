namespace Rooms.Core.Dtos.Requests.OperatorRooms;

public record GetOperatorRoomsDto(int BatchNumber, int BatchSize, int AfterOperatorRoomId, OperatorRoomsFilterDto? Filter);