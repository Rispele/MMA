using Commons.Core.Models.Filtering;
using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Core.Interfaces.Dtos.Room.Requests;

public record RoomsFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
    public FilterParameterDto<string>? Description { get; init; }
    public FilterMultiParameterDto<RoomType>? RoomTypes { get; init; }
    public FilterMultiParameterDto<RoomLayout>? RoomLayout { get; init; }
    public FilterParameterDto<int>? Seats { get; init; }
    public FilterParameterDto<int>? ComputerSeats { get; init; }
    public FilterMultiParameterDto<RoomNetType>? NetTypes { get; init; }

    public FilterParameterDto<bool>? Conditioning { get; init; }

    public FilterMultiParameterDto<int>? OperatorDepartments { get; init; }
    public FilterParameterDto<string>? Operator { get; init; }
    public FilterParameterDto<string>? Owner { get; init; }
    public FilterMultiParameterDto<RoomStatus>? RoomStatuses { get; init; }
    public FilterParameterDto<DateTime>? FixDeadline { get; init; }
    public FilterParameterDto<string>? Comment { get; init; }
}