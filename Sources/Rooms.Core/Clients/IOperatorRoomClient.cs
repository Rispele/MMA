namespace Rooms.Core.Clients;

public interface IOperatorRoomClient
{
    Task<Dictionary<Guid, string>> GetAvailableOperators();
}