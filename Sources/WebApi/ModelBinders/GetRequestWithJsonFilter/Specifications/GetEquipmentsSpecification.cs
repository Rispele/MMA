using WebApi.Models.Requests.Equipments;

namespace WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

public class GetEquipmentsSpecification : IGetRequestWithJsonFilterModelBinderSpecification<GetEquipmentsModel, EquipmentsFilterModel>
{
    public GetEquipmentsModel Build(int pageNumber, int pageSize, int afterId, EquipmentsFilterModel? filter)
    {
        return new GetEquipmentsModel
        {
            AfterId = afterId,
            Filter = filter,
            PageSize = pageSize,
            Page = pageNumber
        };
    }
}