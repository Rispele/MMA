using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.BookingRequest;

internal sealed record GetBookingRequestByIdQuery(int BookingRequestId)
    : ISingleQuerySpecification<GetBookingRequestByIdQuery, Domain.Models.BookingRequests.BookingRequest>;