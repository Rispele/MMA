using Booking.Domain.Models.BookingProcesses.Events;
using Commons.Domain.Queries.Abstractions;

namespace Booking.Core.Queries.BookingProcesses;

public record ReadBookingEventsQuery(int FromEventId, int BatchSize) : IQuerySpecification<ReadBookingEventsQuery, BookingEvent>;