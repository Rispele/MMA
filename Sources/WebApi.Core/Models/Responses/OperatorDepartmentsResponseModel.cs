using WebApi.Core.Models.OperatorDepartments;

namespace WebApi.Core.Models.Responses;

public record OperatorDepartmentsResponseModel(OperatorDepartmentModel[] OperatorDepartments, int TotalCount);