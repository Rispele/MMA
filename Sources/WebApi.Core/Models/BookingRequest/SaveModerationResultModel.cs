namespace WebApi.Core.Models.BookingRequest;

public class SaveModerationResultModel
{
    public bool IsApproved { get; set; }
    public string ModeratorComment { get; set; } = null!;
}