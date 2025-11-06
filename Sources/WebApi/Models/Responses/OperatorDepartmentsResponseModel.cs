using WebApi.Models.OperatorDepartments;

namespace WebApi.Models.Responses;

public class OperatorDepartmentsResponseModel
{
    public OperatorDepartmentModel[] OperatorDepartments { get; init; } = [];
    public int Count { get; init; }
}