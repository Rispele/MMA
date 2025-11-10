namespace Rooms.Core.Clients.Interfaces;

public interface IOperatorDepartmentClient
{
    Task<Dictionary<Guid, string>> GetAvailableOperators();
}