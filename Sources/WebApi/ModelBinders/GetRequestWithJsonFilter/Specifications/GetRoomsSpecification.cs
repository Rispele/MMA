using WebApi.Models.Requests.Rooms;

namespace WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

public class GetRoomsSpecification : IGetRequestWithJsonFilterModelBinderSpecification<GetRoomsModel, RoomsFilterModel>
{
    public GetRoomsModel Build(int pageNumber, int pageSize, int afterId, RoomsFilterModel? filter)
    {
        return new GetRoomsModel
        {
            AfterId = afterId,
            Filter = filter,
            PageSize = pageSize,
            Page = pageNumber
        };
    }
}