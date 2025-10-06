using WebApi.Models.Files;
using WebApi.Models.Room;

namespace WebApi.Models.Requests;

public record PostRoomRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public RoomTypeModel? Type { get; init; }
    public RoomLayoutModel? Layout { get; init; }
    public int? Seats { get; init; }
    public int? ComputerSeats { get; init; }
    public FileMetadataModel? PdfRoomSchemeFileMetadata { get; init; }
    public FileMetadataModel? PhotoFileMetadata { get; init; }
    public RoomNetTypeModel? NetType { get; init; }
    public bool HasConditioning { get; init; }
    public string? Owner { get; init; }
    public RoomStatusModel? RoomStatus { get; init; }
    public string? Comment { get; init; }
    public DateTime? FixDeadline { get; init; }
    public bool AllowBooking { get; init; }
}