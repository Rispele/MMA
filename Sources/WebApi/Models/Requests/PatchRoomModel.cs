using WebApi.Dto.Room;

namespace WebApi.Models.Requests;

public record PatchRoomModel
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public RoomTypeModel? Type { get; init; }
    public RoomLayoutModel? Layout { get; init; }
    public int? Seats { get; init; }
    public int? ComputerSeats { get; init; }
    public byte[]? PdfRoomScheme { get; init; }
    public byte[]? Photo { get; init; }
    public RoomNetTypeModel? NetType { get; init; }
    public bool HasConditioning { get; init; }
    public string? Owner { get; init; }
    public RoomStatusModel? RoomStatus { get; init; }
    public string? Comment { get; init; }
    public DateTime? FixDeadline { get; init; }
    public bool AllowBooking { get; init; }
}