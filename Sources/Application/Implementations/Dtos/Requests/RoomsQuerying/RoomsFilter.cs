using Application.Implementations.Dtos.Requests.Filtering;
using Application.Implementations.Dtos.Room;

namespace Application.Implementations.Dtos.Requests.RoomsQuerying;

public record RoomsFilter
{
    public FilterParameter<string>? Name { get; init; }
    public FilterParameter<string>? Description { get; init; }
    public FilterMultiParameter<RoomTypeDto>? RoomTypes { get; init; }
    public FilterMultiParameter<RoomLayoutDto>? RoomLayout { get; init; }
    public FilterParameter<int>? Seats { get; init; }
    public FilterParameter<int>? ComputerSeats { get; init; }
    public FilterMultiParameter<RoomNetTypeDto>? NetTypes { get; init; }
    public FilterParameter<bool>? Conditioning { get; init; }
    public FilterMultiParameter<int>? OperatorDepartments { get; init; }
    public FilterParameter<string>? Operator { get; init; }
    public FilterParameter<string>? Owner { get; init; }
    public FilterMultiParameter<RoomStatusDto>? RoomStatuses { get; init; }
    // description already present above
}

