using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.BookingRequest;
using Rooms.Domain.Models.BookingRequests;
using Rooms.Infrastructure.Queries.Abstractions;

namespace Rooms.Infrastructure.Queries.BookingRequests;

public readonly struct FindBookingRequestByIdQuery :
    IFindBookingRequestByIdQuery,
    ISingleQueryImplementer<BookingRequest?, RoomsDbContext>
{
    public required int BookingRequestId { get; init; }

    public Task<BookingRequest?> Apply(
        RoomsDbContext source,
        CancellationToken cancellationToken)
    {
        var id = BookingRequestId;

        return source.BookingRequests
            .Include(x => x.Rooms)
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}