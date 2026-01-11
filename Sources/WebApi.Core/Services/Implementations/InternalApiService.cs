using Booking.Core.Interfaces.Services.Booking;
using WebApi.Core.Models.InternalApi;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Core.Services.Implementations;

public class InternalApiService(IBookingService bookingService) : IInternalApiService
{
    public Task SaveEdmsResolutionResult(int bookingRequestId, EdmsResolutionResult edmsResolutionResult, CancellationToken cancellationToken)
    {
        return bookingService.SaveEdmsResolutionResult(bookingRequestId, edmsResolutionResult.IsApproved, cancellationToken);
    }
}