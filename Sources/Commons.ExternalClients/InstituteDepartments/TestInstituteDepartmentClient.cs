namespace Commons.ExternalClients.InstituteDepartments;

public class TestInstituteDepartmentClient : IInstituteDepartmentClient
{
    private static readonly InstituteDepartmentResponseDto[] InstituteDepartments = InstituteDepartmentsTestData.GetInstituteDepartments();

    public Task<InstituteDepartmentResponseDto[]> GetAvailableInstituteDepartments()
    {
        return Task.FromResult(InstituteDepartments);
    }
}