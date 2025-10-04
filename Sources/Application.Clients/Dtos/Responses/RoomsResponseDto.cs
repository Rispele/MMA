using Application.Clients.Dtos.Room;

namespace Application.Clients.Dtos.Responses;

public record RoomsResponseDto(
    RoomDto[] Rooms,
    int Count,
    int? LastRoomId);
