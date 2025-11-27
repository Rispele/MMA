using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Room.Fix;
using Rooms.Core.Interfaces.Dtos.Room.Parameters;

namespace Rooms.Core.Interfaces.Dtos.Room;

public record RoomDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public ScheduleAddressDto? ScheduleAddress { get; init; }
    public required RoomParametersDto Parameters { get; init; }
    public required RoomAttachmentsDto Attachments { get; init; }
    public string? Owner { get; init; }
    public required RoomFixStatusDto FixInfo { get; init; }
    public bool AllowBooking { get; init; }
    public required EquipmentDto[] Equipments { get; init; }
    public int? OperatorDepartmentId { get; init; }
}