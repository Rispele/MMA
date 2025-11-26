namespace Rooms.Core.Dtos.Requests.RoomSchedule;

public record GetRoomScheduleDto(int RoomId, DateOnly From, DateOnly To);