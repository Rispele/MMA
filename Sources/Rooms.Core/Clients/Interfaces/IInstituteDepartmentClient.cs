namespace Rooms.Core.Clients.Interfaces;

public interface IInstituteDepartmentClient
{
    Task<Dictionary<string, string>> GetAvailableInstituteDepartments();
}