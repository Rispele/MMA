using Rooms.Core.Dtos.OperatorRoom;

namespace Rooms.Core.Dtos.Responses;

public record OperatorRoomsResponseDto(
    OperatorRoomDto[] OperatorRooms,
    int Count,
    int? LastOperatorRoomId);