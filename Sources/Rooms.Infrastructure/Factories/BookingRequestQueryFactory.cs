using Rooms.Core.Dtos.Requests.BookingRequests;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.BookingRequest;
using Rooms.Infrastructure.Queries.BookingRequests;

namespace Rooms.Infrastructure.Factories;

public class BookingRequestQueryFactory : IBookingRequestQueryFactory
{
    public IFilterBookingRequestsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterBookingRequestId = -1,
        BookingRequestsFilterDto? filter = null)
    {
        return new FilterBookingRequestsQuery
        {
            BatchSize = batchSize,
            BatchNumber = batchNumber,
            AfterBookingRequestId = afterBookingRequestId,
            Filter = filter
        };
    }

    public IFindBookingRequestByIdQuery FindById(int bookingRequestId)
    {
        return new FindBookingRequestByIdQuery
        {
            BookingRequestId = bookingRequestId
        };
    }
}