using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Core.Interfaces.Dtos.Room.Fix;

public record RoomFixStatusDto(RoomStatus Status, DateTime? FixDeadline, string? Comment);