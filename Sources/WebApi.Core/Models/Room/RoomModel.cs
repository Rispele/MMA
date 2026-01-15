using WebApi.Core.Models.Equipment;
using WebApi.Core.Models.OperatorDepartments;
using WebApi.Core.Models.Room.Fix;
using WebApi.Core.Models.Room.Parameters;

namespace WebApi.Core.Models.Room;

public record RoomModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; } = string.Empty;
    public ScheduleAddressModel? ScheduleAddress { get; init; }
    public required RoomParametersModel Parameters { get; init; }
    public required RoomAttachmentsModel Attachments { get; init; }
    public string? Owner { get; init; }
    public required RoomFixStatusModel FixInfo { get; init; }
    public bool AllowBooking { get; init; }
    public IEnumerable<EquipmentModel> Equipments { get; init; } = Enumerable.Empty<EquipmentModel>();
    public int? OperatorDepartmentId { get; init; }
    public OperatorDepartmentModel? OperatorDepartment { get; init; }
}