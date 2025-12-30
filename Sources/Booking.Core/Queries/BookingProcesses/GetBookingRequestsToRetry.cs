using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.BookingProcesses;

public record GetBookingRequestsToRetry(int BatchSize) : IQuerySpecification<
    GetBookingRequestsToRetry, Domain.Models.BookingRequests.BookingRequest>;