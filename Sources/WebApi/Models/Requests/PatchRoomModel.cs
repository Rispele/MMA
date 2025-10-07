using WebApi.Models.Room;

namespace WebApi.Models.Requests;

public record PatchRoomModel
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public RoomTypeModel Type { get; set; }
    public RoomLayoutModel Layout { get; set; }
    public RoomNetTypeModel NetType { get; set; }
    public int? Seats { get; set; }
    public int? ComputerSeats { get; set; }
    public bool HasConditioning { get; set; }
    public string? Owner { get; set; }
    public RoomStatusModel RoomStatus { get; set; }
    public string? Comment { get; set; }
    public DateTime? FixDeadline { get; set; }
    public bool AllowBooking { get; set; }
}