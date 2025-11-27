namespace Commons.ExternalClients.InstituteDepartments;

public interface IInstituteDepartmentClient
{
    Task<Dictionary<string, string>> GetAvailableInstituteDepartments();
}