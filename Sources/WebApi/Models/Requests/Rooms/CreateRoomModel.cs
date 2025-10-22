using System.ComponentModel.DataAnnotations;
using WebApi.Models.Files;
using WebApi.Models.Room;

namespace WebApi.Models.Requests.Rooms;

public record CreateRoomModel
{
    [Required]
    [Length(1, 64, ErrorMessage = "Длина имени должна быть от 1 до 64 символов включительно")]
    [RegularExpression("[a-zA-Zа-яА-Я0-9,\\.\\-#№\\(\\)]{0,64}", ErrorMessage = "В поле Name переданы недоступные символы")]
    public string Name { get; init; } = string.Empty;

    [Length(1, 256, ErrorMessage = "Длина имени должна быть от 1 до 256 символов включительно")]
    public string? Description { get; init; }

    public RoomTypeModel Type { get; init; }
    public RoomLayoutModel Layout { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? Seats { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? ComputerSeats { get; init; }

    public FileMetadataModel? PdfRoomSchemeFileMetadata { get; init; }
    public FileMetadataModel? PhotoFileMetadata { get; init; }
    public RoomNetTypeModel NetType { get; init; }
    public bool HasConditioning { get; init; }

    [Length(1, 64, ErrorMessage = "Длина владельца должна быть от 1 до 64 символов включительно")]
    public string? Owner { get; init; }

    public RoomStatusModel RoomStatus { get; init; }

    [Length(1, 256, ErrorMessage = "Длина комментария должна быть от 1 до 256 символов включительно")]
    public string? Comment { get; init; }

    public DateTime? FixDeadline { get; init; }
    public bool AllowBooking { get; init; }
}