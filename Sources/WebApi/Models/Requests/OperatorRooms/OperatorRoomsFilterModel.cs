using WebApi.Models.Requests.Filtering;

namespace WebApi.Models.Requests.OperatorRooms;

public record OperatorRoomsFilterModel
{
    public FilterParameterModel<string>? Name { get; init; }
    public FilterParameterModel<string>? RoomName { get; init; }
    public FilterParameterModel<string>? Operator { get; init; }
    public FilterParameterModel<string>? OperatorEmail { get; init; }
    public FilterParameterModel<string>? Contacts { get; init; }
}