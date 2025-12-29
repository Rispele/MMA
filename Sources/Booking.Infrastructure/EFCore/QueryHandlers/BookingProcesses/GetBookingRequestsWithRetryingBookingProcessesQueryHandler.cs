using Booking.Core.Queries.BookingProcesses;
using Booking.Domain.Models.BookingProcesses;
using Booking.Domain.Models.BookingRequests;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingProcesses;

internal class GetBookingRequestsWithRetryingBookingProcessesQueryHandler 
    : IQueryHandler<BookingDbContext, GetBookingRequestsWithRetryingBookingProcesses, BookingRequest>
{
    public Task<IAsyncEnumerable<BookingRequest>> Handle(EntityQuery<BookingDbContext, GetBookingRequestsWithRetryingBookingProcesses, IAsyncEnumerable<BookingRequest>> request, CancellationToken cancellationToken)
    {
        var enumerable = request.Context.BookingRequests
            .Where(t => t.BookingProcess != null && t.BookingProcess.State == BookingProcessState.Retrying)
            .Take(request.Query.BatchSize)
            .AsAsyncEnumerable();
        
        return Task.FromResult(enumerable);
    }
}