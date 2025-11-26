using WebApi.Models.Requests.Filtering;

namespace WebApi.Models.Requests.OperatorDepartments;

public record OperatorDepartmentsFilterModel
{
    public FilterParameterModel<string>? Name { get; init; }
    public FilterParameterModel<string>? RoomName { get; init; }
    public FilterParameterModel<string>? Operator { get; init; }
    // public FilterParameterModel<string>? OperatorEmail { get; init; }
    // public FilterParameterModel<string>? Contacts { get; init; }
}