using Booking.Domain.Models.BookingProcesses.Events;

namespace Booking.Core.Services.Booking.KnownProcessors.Result;

public record ProcessorResult(BookingEvent BookingEvent, ResultType Result);