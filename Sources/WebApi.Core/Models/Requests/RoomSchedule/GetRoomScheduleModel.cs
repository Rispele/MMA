namespace WebApi.Core.Models.Requests.RoomSchedule;

public record GetRoomScheduleModel(int RoomId, DateOnly From, DateOnly To);