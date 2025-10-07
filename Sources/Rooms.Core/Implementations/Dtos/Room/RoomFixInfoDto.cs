namespace Rooms.Core.Implementations.Dtos.Room;

public record RoomFixInfoDto(RoomStatusDto Status, DateTime? FixDeadline, string? Comment);
