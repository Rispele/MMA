using WebApi.Models.Requests.OperatorDepartments;

namespace WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

public class GetOperatorDepartmentsSpecification : IGetRequestWithJsonFilterModelBinderSpecification<GetOperatorDepartmentsModel, OperatorDepartmentsFilterModel>
{
    public GetOperatorDepartmentsModel Build(int pageNumber, int pageSize, int afterId, OperatorDepartmentsFilterModel? filter)
    {
        return new GetOperatorDepartmentsModel
        {
            AfterId = afterId,
            Filter = filter,
            PageSize = pageSize,
            Page = pageNumber
        };
    }
}