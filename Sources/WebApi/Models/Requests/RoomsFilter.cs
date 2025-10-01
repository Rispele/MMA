using WebApi.Models.Requests.Filtering;
using WebApi.Models.Room;

namespace WebApi.Models.Requests;

public record RoomsFilter
{
    public FilterParameter<string>? Name { get; init; }
    public FilterParameter<string>? Description { get; init; }
    public FilterMultiParameter<RoomTypeModel>? RoomTypes { get; init; }
    public FilterMultiParameter<RoomLayoutModel>? RoomLayout { get; init; }
    public FilterParameter<int>? Seats { get; init; }
    public FilterParameter<int>? ComputerSeats { get; init; }
    public FilterMultiParameter<RoomNetTypeModel>? NetTypes { get; init; }
    public FilterParameter<bool>? Conditioning { get; init; }
    public FilterMultiParameter<int>? OperatorDepartments { get; init; }
    public FilterParameter<string>? Operator { get; init; }
    public FilterParameter<string>? Owner { get; init; }
    public FilterMultiParameter<RoomStatusModel>? RoomStatuses { get; init; }
    // description already present above
}

