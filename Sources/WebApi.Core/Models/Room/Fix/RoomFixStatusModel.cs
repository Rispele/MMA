using Rooms.Domain.Propagated.Rooms;

namespace WebApi.Core.Models.Room.Fix;

public record RoomFixStatusModel(RoomStatus Status, DateTime? FixDeadline, string? Comment);