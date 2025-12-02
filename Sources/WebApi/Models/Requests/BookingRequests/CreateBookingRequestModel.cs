using Booking.Domain.Propagated.BookingRequests;
using JetBrains.Annotations;
using WebApi.Models.BookingRequest;
using WebApi.Models.BookingRequest.RoomEventCoordinator;

namespace WebApi.Models.Requests.BookingRequests;

public record CreateBookingRequestModel
{
    public BookingCreatorModel Creator { get; [UsedImplicitly] set; } = null!;
    public string Reason { get; [UsedImplicitly] set; } = null!;
    public int ParticipantsCount { get; [UsedImplicitly] set; }
    public bool TechEmployeeRequired { get; [UsedImplicitly] set; }
    public string EventHostFullName { get; [UsedImplicitly] set; } = null!;
    public required IRoomEventCoordinatorModel RoomEventCoordinator { get; init; }
    public string EventName { get; [UsedImplicitly] set; } = null!;
    public IEnumerable<int> RoomIds { get; [UsedImplicitly] set; } = null!;
    public IEnumerable<BookingTimeModel> BookingSchedule { get; [UsedImplicitly] set; } = [];
    public string? ModeratorComment { get; [UsedImplicitly] set; } = null!;
    public BookingScheduleStatus? BookingScheduleStatus { get; [UsedImplicitly] set; }
}