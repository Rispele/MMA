namespace Rooms.Core.Clients;

public interface IOperatorDepartmentClient
{
    Task<Dictionary<Guid, string>> GetAvailableOperators();
}