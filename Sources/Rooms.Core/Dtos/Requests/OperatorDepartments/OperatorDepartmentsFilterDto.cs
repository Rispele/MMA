using Rooms.Core.Dtos.Requests.Filtering;

namespace Rooms.Core.Dtos.Requests.OperatorDepartments;

public record OperatorDepartmentsFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
    public FilterParameterDto<string>? RoomName { get; init; }
    public FilterParameterDto<string>? Operator { get; init; }
    // public FilterParameterDto<string>? OperatorEmail { get; init; }
    // public FilterParameterDto<string>? Contacts { get; init; }
}