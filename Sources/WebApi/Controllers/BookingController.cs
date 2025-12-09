using Booking.Core.Interfaces.Services.Booking;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/booking")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpPost]
    [Route("approval/{bookingRequestId:int}")]
    public Task SendForApprovalInEdms([FromQuery] int bookingRequestId, CancellationToken cancellationToken)
    {
        return bookingService.SendBookingRequestForApprovalInEdms(bookingRequestId, cancellationToken);
    }
}