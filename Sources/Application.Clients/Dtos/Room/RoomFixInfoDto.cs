namespace Application.Clients.Dtos.Room;

public record RoomFixInfoDto(RoomStatusDto Status, DateTime? FixDeadline, string? Comment);
