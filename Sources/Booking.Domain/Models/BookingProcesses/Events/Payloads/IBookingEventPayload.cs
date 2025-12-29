using System.Text.Json.Serialization;

namespace Booking.Domain.Models.BookingProcesses.Events.Payloads;

[JsonDerivedType(typeof(BookingRequestInitiatedEventPayload), nameof(BookingRequestInitiatedEventPayload))]
[JsonDerivedType(typeof(BookingRequestRoomBookedForDayEventPayload), nameof(BookingRequestRoomBookedForDayEventPayload))]
[JsonDerivedType(typeof(BookingRequestSentForApprovalInEdmsEventPayload), nameof(BookingRequestSentForApprovalInEdmsEventPayload))]
[JsonDerivedType(typeof(BookingRequestResolvedInEdmsEventPayload), nameof(BookingRequestResolvedInEdmsEventPayload))]
public interface IBookingEventPayload
{
    public TPayload GetPayload<TPayload>() where TPayload : class, IBookingEventPayload
    {
        return this as TPayload ?? throw new InvalidCastException($"Could not cast payload to [{typeof(TPayload).FullName}]");
    }
}