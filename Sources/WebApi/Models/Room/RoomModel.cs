using WebApi.Models.Equipment;

namespace WebApi.Models.Room;

public record RoomModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; } = string.Empty;
    public ScheduleAddressModel? ScheduleAddress { get; init; }
    public required RoomParametersModel Parameters { get; init; }
    public required RoomAttachmentsModel Attachments { get; init; }
    public string? Owner { get; init; }
    public RoomOperatorDepartmentModel? OperatorDepartment { get; init; }
    public required RoomFixStatusModel FixStatus { get; init; }
    public bool AllowBooking { get; init; }
    public IEnumerable<EquipmentModel> Equipments { get; init; } = [];
}