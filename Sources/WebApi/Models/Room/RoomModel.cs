namespace WebApi.Models.Room;

public record RoomModel
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public ScheduleAddressModel? ScheduleAddress { get; init; }
    public RoomParametersModel? Parameters { get; init; }
    public RoomAttachmentsModel? Attachments { get; init; }
    public string Owner { get; init; } = string.Empty;
    public RoomOperatorDepartmentModel? OperatorDepartment { get; init; }
    public RoomFixStatusModel? FixStatus { get; init; }
    public bool AllowBooking { get; init; }
}