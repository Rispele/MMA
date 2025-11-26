namespace Rooms.Core.Dtos.Room.Responses;

public record RoomsResponseDto(
    RoomDto[] Rooms,
    int Count,
    int? LastRoomId);