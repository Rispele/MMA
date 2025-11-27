using Microsoft.Extensions.DependencyInjection;

namespace Rooms.Core;

public class RoomsScopeAttribute() : FromKeyedServicesAttribute(KnownScopes.Rooms);