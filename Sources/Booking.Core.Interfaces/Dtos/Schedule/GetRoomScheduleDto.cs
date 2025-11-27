namespace Booking.Core.Interfaces.Dtos.Schedule;

public record GetRoomScheduleDto(int RoomId, DateOnly From, DateOnly To);