using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.BookingProcesses;

public record GetBookingRequestsToRollback(int BatchSize) : IQuerySpecification<GetBookingRequestsToRollback, Domain.Models.BookingRequests.BookingRequest>;