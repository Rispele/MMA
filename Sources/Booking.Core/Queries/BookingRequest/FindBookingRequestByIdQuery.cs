using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.BookingRequest;

public sealed record FindBookingRequestByIdQuery(int BookingRequestId)
    : ISingleQuerySpecification<FindBookingRequestByIdQuery, Domain.Models.BookingRequests.BookingRequest>;