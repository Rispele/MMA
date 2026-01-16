using WebApi.Core.Models.OperatorDepartments;

namespace WebApi.Core.Models.Responses;

public class OperatorDepartmentsResponseModel(OperatorDepartmentModel[] OperatorDepartments, int TotalCount);