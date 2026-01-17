namespace WebApi.Core.Models.Room;

public record ScheduleAddressModel
{
    public required string RoomNumber { get; init; }
    public required string Address { get; init; }
    public required int ScheduleRoomId { get; init; }
}