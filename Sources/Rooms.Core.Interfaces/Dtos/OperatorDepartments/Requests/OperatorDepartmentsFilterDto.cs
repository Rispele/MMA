using Commons.Core.Models.Filtering;

namespace Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;

public record OperatorDepartmentsFilterDto
{
    public FilterParameterDto<string>? Name { get; init; }
    public FilterParameterDto<string>? RoomName { get; init; }
    public FilterParameterDto<string>? Operator { get; init; }
    // public FilterParameterDto<string>? OperatorEmail { get; init; }
    // public FilterParameterDto<string>? Contacts { get; init; }
}