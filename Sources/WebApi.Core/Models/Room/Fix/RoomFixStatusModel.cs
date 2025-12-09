namespace WebApi.Core.Models.Room.Fix;

public record RoomFixStatusModel(RoomStatusModel Status, DateTime? FixDeadline, string? Comment);