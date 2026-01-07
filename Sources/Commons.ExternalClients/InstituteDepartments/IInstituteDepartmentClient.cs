namespace Commons.ExternalClients.InstituteDepartments;

public interface IInstituteDepartmentClient
{
    Task<InstituteDepartmentResponseDto[]> GetAvailableInstituteDepartments();
}