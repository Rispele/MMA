using Rooms.Core.Implementations.Dtos.Room;

namespace Rooms.Core.Implementations.Dtos.Responses;

public record RoomsBatchDto(
    RoomDto[] Rooms,
    int Count,
    int? LastRoomId);
