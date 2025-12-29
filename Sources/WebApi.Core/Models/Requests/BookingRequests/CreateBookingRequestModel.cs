using Booking.Domain.Propagated.BookingRequests;
using WebApi.Core.Models.BookingRequest;
using WebApi.Core.Models.BookingRequest.RoomEventCoordinator;

namespace WebApi.Core.Models.Requests.BookingRequests;

public record CreateBookingRequestModel
{
    public BookingCreatorModel Creator { get; set; } = null!;
    public string Reason { get; set; } = null!;
    public int ParticipantsCount { get; set; }
    public bool TechEmployeeRequired { get; set; }
    public string EventHostFullName { get; set; } = null!;
    public required IRoomEventCoordinatorModel RoomEventCoordinator { get; init; }
    public string EventName { get; set; } = null!;
    public IEnumerable<BookingTimeModel> BookingSchedule { get; set; } = [];
}