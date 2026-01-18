using Rooms.Domain.Propagated.Rooms;
using WebApi.Core.Models.Requests.Filtering;

namespace WebApi.Core.Models.Requests.Rooms;

public record RoomsFilterModel
{
    public FilterParameterModel<string>? Name { get; init; }
    public FilterParameterModel<string>? Description { get; init; }
    public FilterMultiParameterModel<RoomType>? RoomTypes { get; init; }
    public FilterMultiParameterModel<RoomLayout>? RoomLayout { get; init; }
    public FilterParameterModel<int>? Seats { get; init; }
    public FilterParameterModel<int>? ComputerSeats { get; init; }
    public FilterMultiParameterModel<RoomNetType>? NetTypes { get; init; }
    public FilterParameterModel<bool>? Conditioning { get; init; }
    public FilterMultiParameterModel<int>? OperatorDepartments { get; init; }
    public FilterParameterModel<string>? Operator { get; init; }
    public FilterParameterModel<string>? Owner { get; init; }
    public FilterMultiParameterModel<RoomStatus>? RoomStatuses { get; init; }
    public FilterParameterModel<DateTime>? FixDeadline { get; init; }
    public FilterParameterModel<string>? Comment { get; init; }
}