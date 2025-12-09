using System.Text.Json.Serialization;

namespace Booking.Domain.Events.Payloads;

[JsonDerivedType(typeof(BookingRequestSentForApprovalInEdmsEventPayload), nameof(BookingRequestSentForApprovalInEdmsEventPayload))]
public interface IBookingRequestEventPayload;