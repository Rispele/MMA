namespace WebApi.Models.Room;

public record RoomFixStatusModel(RoomStatusModel Status, DateTime FixDeadline, string Comment);
