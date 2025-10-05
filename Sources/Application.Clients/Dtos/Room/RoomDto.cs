namespace Application.Clients.Dtos.Room;

public record RoomDto
{
    public int Id { get; }
    public string Name { get; }
    public string? Description { get; }
    public ScheduleAddressDto? ScheduleAddress { get; }
    public RoomParametersDto Parameters { get; }
    public RoomAttachmentsDto Attachments { get; }
    public string? Owner { get; }
    public RoomOperatorDepartmentDto? OperatorDepartment { get; }
    public RoomFixInfoDto FixInfo { get; }
    public bool AllowBooking { get; }

    public RoomDto(
        int id,
        string name,
        string? description,
        ScheduleAddressDto? scheduleAddress,
        RoomParametersDto parameters,
        RoomAttachmentsDto attachments,
        string? owner,
        RoomOperatorDepartmentDto? operatorDepartment,
        RoomFixInfoDto fixInfo,
        bool allowBooking)
    {
        Id = id;
        Name = name;
        Description = description;
        ScheduleAddress = scheduleAddress;
        Parameters = parameters;
        Attachments = attachments;
        OperatorDepartment = operatorDepartment;
        FixInfo = fixInfo;
        AllowBooking = allowBooking;
        Owner = owner;
    }
}