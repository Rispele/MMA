using Booking.Core.Queries.BookingProcesses;
using Booking.Domain.Models.BookingProcesses;
using Booking.Domain.Models.BookingRequests;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingProcesses;

internal class GetBookingRequestsToRollbackQueryHandler
    : IQueryHandler<BookingDbContext, GetBookingRequestsToRollback, BookingRequest>
{
    public Task<IAsyncEnumerable<BookingRequest>> Handle(EntityQuery<BookingDbContext, GetBookingRequestsToRollback, IAsyncEnumerable<BookingRequest>> request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var enumerable = request.Context.BookingRequests
            .Where(t => t.BookingProcess != null
                        && t.BookingProcess.State == BookingProcessState.RollingBack
                        && t.BookingProcess.RollbackAt < now)
            .Take(request.Query.BatchSize)
            .AsAsyncEnumerable();

        return Task.FromResult(enumerable);
    }
}