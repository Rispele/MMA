namespace Rooms.Core.Clients.Interfaces;

public interface IOperatorDepartmentClient
{
    Task<Dictionary<string, string>> GetAvailableOperators();
}