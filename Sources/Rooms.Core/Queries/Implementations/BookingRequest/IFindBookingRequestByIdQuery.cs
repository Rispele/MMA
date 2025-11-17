using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.BookingRequest;

public interface IFindBookingRequestByIdQuery : ISingleQuerySpecification<Domain.Models.BookingRequests.BookingRequest>
{
    public int BookingRequestId { get; init; }
}