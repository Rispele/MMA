namespace Booking.Core.Dtos.Schedule;

public record GetRoomScheduleDto(int RoomId, DateOnly From, DateOnly To);