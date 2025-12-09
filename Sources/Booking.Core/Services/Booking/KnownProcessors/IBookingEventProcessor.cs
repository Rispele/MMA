using Booking.Domain.Events;
using Booking.Domain.Events.Payloads;
using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Services.Booking.KnownProcessors;

public interface IBookingEventProcessor<out TEventPayload>
    where TEventPayload : IBookingEventPayload
{
    public Type PayloadType { get; }
    
    public Task ProcessEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken);
}