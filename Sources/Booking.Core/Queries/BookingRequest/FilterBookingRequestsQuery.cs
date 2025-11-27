using Booking.Core.Interfaces.Dtos.BookingRequest.Requests;
using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.BookingRequest;

internal sealed record FilterBookingRequestsQuery(
    int BatchSize,
    int BatchNumber,
    int AfterBookingRequestId,
    BookingRequestsFilterDto? Filter)
    : IQuerySpecification<FilterBookingRequestsQuery, Domain.Models.BookingRequests.BookingRequest>;