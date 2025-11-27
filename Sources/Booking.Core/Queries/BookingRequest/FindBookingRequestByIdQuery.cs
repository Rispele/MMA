using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.BookingRequest;

public sealed record FindBookingRequestByIdQuery(int BookingRequestId)
    : ISingleQuerySpecification<FindBookingRequestByIdQuery, Domain.Models.BookingRequests.BookingRequest>;