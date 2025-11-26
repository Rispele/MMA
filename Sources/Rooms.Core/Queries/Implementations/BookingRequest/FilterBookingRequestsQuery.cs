using Rooms.Core.Dtos.BookingRequest.Requests;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.BookingRequest;

public sealed record FilterBookingRequestsQuery(
    int BatchSize,
    int BatchNumber,
    int AfterBookingRequestId,
    BookingRequestsFilterDto? Filter)
    : IQuerySpecification<FilterBookingRequestsQuery, Domain.Models.BookingRequests.BookingRequest>;