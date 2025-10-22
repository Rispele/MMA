namespace Rooms.Core.Dtos.Room;

public record RoomFixStatusDto(RoomStatusDto Status, DateTime? FixDeadline, string? Comment);
