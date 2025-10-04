using Application.Clients.Dtos.Room;

namespace Application.Clients.Dtos.Requests.RoomPatching;

public record PatchRoomRequest
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public RoomTypeDto Type { get; init; }
    public RoomLayoutDto Layout { get; init; }
    public int? Seats { get; init; }
    public int? ComputerSeats { get; init; }

    // public byte[]? PdfRoomScheme { get; init; }
    // public byte[]? Photo { get; init; }
    public RoomNetTypeDto NetType { get; init; }
    public bool HasConditioning { get; init; }
    public string? Owner { get; init; }
    public RoomStatusDto RoomStatus { get; init; }
    public string? Comment { get; init; }
    public DateTime? FixDeadline { get; init; }
    public bool AllowBooking { get; init; }
}