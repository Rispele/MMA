using Rooms.Domain.Models.RoomModels.Fix;
using Rooms.Domain.Models.RoomModels.Parameters;
using Rooms.Domain.Queries.Implementations.Filtering;

namespace Rooms.Domain.Queries.Implementations.Rooms;

public record RoomsFilter
{
    public FilterParameter<string>? Name { get; init; }
    public FilterParameter<string>? Description { get; init; }
    public FilterMultiParameter<RoomType>? RoomTypes { get; init; }
    public FilterMultiParameter<RoomLayout>? RoomLayout { get; init; }
    public FilterParameter<int>? Seats { get; init; }
    public FilterParameter<int>? ComputerSeats { get; init; }
    public FilterMultiParameter<RoomNetType>? NetTypes { get; init; }
    public FilterParameter<bool>? Conditioning { get; init; }
    // public FilterMultiParameterDto<int>? OperatorDepartments { get; init; }
    // public FilterParameterDto<string>? Operator { get; init; }
    public FilterParameter<string>? Owner { get; init; }
    public FilterMultiParameter<RoomStatus>? RoomStatuses { get; init; }
    public FilterParameter<DateTime>? FixDeadline { get; init; }
    public FilterParameter<string>? Comment { get; init; }
}

