using Booking.Core.Interfaces.Services.Booking;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.BookingRequest;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/booking")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpPost]
    [Route("initiate/{bookingRequestId:int}")]
    public Task Initiate(int bookingRequestId, CancellationToken cancellationToken)
    {
        return bookingService.InitiateBookingRequest(bookingRequestId, cancellationToken);
    }

    [HttpPost]
    [Route("save/{bookingRequestId:int}")]
    public Task Save(int bookingRequestId, [FromBody] SaveModerationResultModel model, CancellationToken cancellationToken)
    {
        return bookingService.SaveModerationResult(bookingRequestId, model.IsApproved, model.ModeratorComment, cancellationToken);
    }
}