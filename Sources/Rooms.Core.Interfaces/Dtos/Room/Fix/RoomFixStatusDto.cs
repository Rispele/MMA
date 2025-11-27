namespace Rooms.Core.Interfaces.Dtos.Room.Fix;

public record RoomFixStatusDto(RoomStatusDto Status, DateTime? FixDeadline, string? Comment);