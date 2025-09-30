namespace WebApi.Dto.Room;

public record RoomFixStatusModel(RoomStatusModel Status, DateTime FixDeadline, string Comment);
