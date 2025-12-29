using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.BookingProcesses;

public record GetBookingRequestsWithRetryingBookingProcesses(int BatchSize) : IQuerySpecification<
    GetBookingRequestsWithRetryingBookingProcesses, Domain.Models.BookingRequests.BookingRequest>;