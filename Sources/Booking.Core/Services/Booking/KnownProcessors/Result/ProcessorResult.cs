using Booking.Domain.Events;

namespace Booking.Core.Services.Booking.KnownProcessors.Result;

public record ProcessorResult(BookingEvent BookingEvent, ResultType Result);