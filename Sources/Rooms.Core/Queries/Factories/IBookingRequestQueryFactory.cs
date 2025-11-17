using Rooms.Core.Dtos.Requests.BookingRequests;
using Rooms.Core.Queries.Implementations.BookingRequest;

namespace Rooms.Core.Queries.Factories;

public interface IBookingRequestQueryFactory
{
    public IFilterBookingRequestsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterBookingRequestId = -1,
        BookingRequestsFilterDto? filter = null);

    public IFindBookingRequestByIdQuery FindById(int bookingRequestId);
}