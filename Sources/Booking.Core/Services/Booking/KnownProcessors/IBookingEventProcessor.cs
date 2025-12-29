using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Services.Booking.KnownProcessors;

public interface IBookingEventProcessor<out TEventPayload>
    where TEventPayload : IBookingEventPayload
{
    public Type PayloadType { get; }
    
    public Task<ProcessorResult> ProcessEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken);
    
    public Task RollbackEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken);
}