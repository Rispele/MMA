using WebApi.Models.Requests.BookingRequests;

namespace WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

public class GetBookingRequestSpecification : IGetRequestWithJsonFilterModelBinderSpecification<GetBookingRequestsModel, BookingRequestsFilterModel>
{
    public GetBookingRequestsModel Build(int pageNumber, int pageSize, int afterId, BookingRequestsFilterModel? filter)
    {
        return new GetBookingRequestsModel
        {
            AfterId = afterId,
            Filter = filter,
            PageSize = pageSize,
            Page = pageNumber
        };
    }
}