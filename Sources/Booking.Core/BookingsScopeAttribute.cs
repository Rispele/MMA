using Microsoft.Extensions.DependencyInjection;

namespace Booking.Core;

public class BookingsScopeAttribute() : FromKeyedServicesAttribute(KnownScopes.Booking);