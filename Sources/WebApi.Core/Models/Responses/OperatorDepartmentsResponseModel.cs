using WebApi.Core.Models.OperatorDepartments;

namespace WebApi.Core.Models.Responses;

public class OperatorDepartmentsResponseModel
{
    public OperatorDepartmentModel[] OperatorDepartments { get; init; } = [];
    public int Count { get; init; }
}