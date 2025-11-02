namespace Rooms.Core.Dtos.Room.Fix;

public record RoomFixStatusDto(RoomStatusDto Status, DateTime? FixDeadline, string? Comment);