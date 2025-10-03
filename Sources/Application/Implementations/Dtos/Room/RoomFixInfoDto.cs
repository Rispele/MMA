namespace Application.Implementations.Dtos.Room;

public record RoomFixInfoDto(RoomStatusDto Status, DateTime? FixDeadline, string? Comment);
