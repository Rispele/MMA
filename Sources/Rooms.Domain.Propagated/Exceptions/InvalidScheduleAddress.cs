using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class InvalidScheduleAddress(string message) : DomainException(400, "ScheduleRoomNotFound", message)
{
    
}