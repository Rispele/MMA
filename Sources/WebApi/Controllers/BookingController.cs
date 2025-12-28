using Booking.Core.Interfaces.Services.Booking;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/booking")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpPost]
    [Route("initiate/{bookingRequestId:int}")]
    public Task Initiate([FromQuery] int bookingRequestId, CancellationToken cancellationToken)
    {
        return bookingService.InitiateBookingRequest(bookingRequestId, cancellationToken);
    }
}