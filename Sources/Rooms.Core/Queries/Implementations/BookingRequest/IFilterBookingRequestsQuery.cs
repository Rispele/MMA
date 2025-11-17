using Rooms.Core.Dtos.Requests.BookingRequests;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.BookingRequest;

public interface IFilterBookingRequestsQuery : IQuerySpecification<Domain.Models.BookingRequests.BookingRequest>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterBookingRequestId { get; init; }
    public BookingRequestsFilterDto? Filter { get; init; }
}