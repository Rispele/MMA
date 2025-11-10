namespace Rooms.Core.Clients.Interfaces;

public interface IInstituteResponsibleClient
{
    Task<Dictionary<string, string>> GetAvailableInstituteResponsible();
}