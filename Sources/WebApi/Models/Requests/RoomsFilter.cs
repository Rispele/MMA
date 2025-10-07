using WebApi.Models.Requests.Filtering;
using WebApi.Models.Room;

namespace WebApi.Models.Requests;

public record RoomsFilter
{
    public FilterParameterModel<string>? Name { get; init; }
    public FilterParameterModel<string>? Description { get; init; }
    public FilterMultiParameterModel<RoomTypeModel>? RoomTypes { get; init; }
    public FilterMultiParameterModel<RoomLayoutModel>? RoomLayout { get; init; }
    public FilterParameterModel<int>? Seats { get; init; }
    public FilterParameterModel<int>? ComputerSeats { get; init; }
    public FilterMultiParameterModel<RoomNetTypeModel>? NetTypes { get; init; }
    public FilterParameterModel<bool>? Conditioning { get; init; }
    public FilterMultiParameterModel<int>? OperatorDepartments { get; init; }
    public FilterParameterModel<string>? Operator { get; init; }
    public FilterParameterModel<string>? Owner { get; init; }
    public FilterMultiParameterModel<RoomStatusModel>? RoomStatuses { get; init; }
    // description already present above
}

