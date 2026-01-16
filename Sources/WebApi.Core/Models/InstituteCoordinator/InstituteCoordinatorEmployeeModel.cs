namespace WebApi.Core.Models.InstituteCoordinator;

public class InstituteCoordinatorEmployeeModel
{
    public string UserId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public Guid InstituteId { get; set; }
}