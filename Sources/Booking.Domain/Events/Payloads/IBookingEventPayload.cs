using System.Text.Json.Serialization;

namespace Booking.Domain.Events.Payloads;

[JsonDerivedType(typeof(BookingRequestSentForApprovalInEdmsEventPayload), nameof(BookingRequestSentForApprovalInEdmsEventPayload))]
public interface IBookingEventPayload
{
    public TPayload GetPayload<TPayload>() where TPayload : class, IBookingEventPayload
    {
        return this as TPayload ?? throw new InvalidCastException($"Could not cast payload to [{typeof(TPayload).FullName}]");
    }
}