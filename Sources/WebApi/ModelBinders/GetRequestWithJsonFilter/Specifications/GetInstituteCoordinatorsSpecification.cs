using WebApi.Models.Requests.InstituteResponsible;

namespace WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

public class GetInstituteCoordinatorsSpecification : IGetRequestWithJsonFilterModelBinderSpecification<GetInstituteCoordinatorModel, InstituteCoordinatorFilterModel>
{
    public GetInstituteCoordinatorModel Build(int pageNumber, int pageSize, int afterId, InstituteCoordinatorFilterModel? filter)
    {
        return new GetInstituteCoordinatorModel
        {
            AfterId = afterId,
            Filter = filter,
            PageSize = pageSize,
            Page = pageNumber
        };
    }
}