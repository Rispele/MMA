using Application.Implementations.Dtos.Requests.Filtering;
using Application.Implementations.Dtos.Room;

namespace Application.Implementations.Dtos.Requests.RoomsQuerying;

public record RoomsFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
    public FilterParameterDto<string>? Description { get; init; }
    public FilterMultiParameterDto<RoomTypeDto>? RoomTypes { get; init; }
    public FilterMultiParameterDto<RoomLayoutDto>? RoomLayout { get; init; }
    public FilterParameterDto<int>? Seats { get; init; }
    public FilterParameterDto<int>? ComputerSeats { get; init; }
    public FilterMultiParameterDto<RoomNetTypeDto>? NetTypes { get; init; }
    public FilterParameterDto<bool>? Conditioning { get; init; }
    // public FilterMultiParameterDto<int>? OperatorDepartments { get; init; }
    // public FilterParameterDto<string>? Operator { get; init; }
    public FilterParameterDto<string>? Owner { get; init; }
    public FilterMultiParameterDto<RoomStatusDto>? RoomStatuses { get; init; }
    public FilterParameterDto<DateTime>? FixDeadline { get; init; }
    public FilterParameterDto<string>? Comment { get; init; }
}

