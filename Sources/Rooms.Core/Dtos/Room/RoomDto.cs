namespace Rooms.Core.Dtos.Room;

public record RoomDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public ScheduleAddressDto? ScheduleAddress { get; init; }
    public RoomParametersDto Parameters { get; init; }
    public RoomAttachmentsDto Attachments { get; init; }
    public string? Owner { get; init; }
    public RoomOperatorDepartmentDto? OperatorDepartment { get; init; }
    public RoomFixStatusDto FixStatus { get; init; }
    public bool AllowBooking { get; init; }

    public RoomDto(
        int id,
        string name,
        string? description,
        ScheduleAddressDto? scheduleAddress,
        RoomParametersDto parameters,
        RoomAttachmentsDto attachments,
        string? owner,
        RoomOperatorDepartmentDto? operatorDepartment,
        RoomFixStatusDto fixStatus,
        bool allowBooking)
    {
        Id = id;
        Name = name;
        Description = description;
        ScheduleAddress = scheduleAddress;
        Parameters = parameters;
        Attachments = attachments;
        OperatorDepartment = operatorDepartment;
        FixStatus = fixStatus;
        AllowBooking = allowBooking;
        Owner = owner;
    }
}