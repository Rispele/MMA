using Rooms.Core.Dtos.Filtering;

namespace Rooms.Core.Dtos.OperatorDepartments.Requests;

public record OperatorDepartmentsFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
    public FilterParameterDto<string>? RoomName { get; init; }
    public FilterParameterDto<string>? Operator { get; init; }
    // public FilterParameterDto<string>? OperatorEmail { get; init; }
    // public FilterParameterDto<string>? Contacts { get; init; }
}