using System.ComponentModel.DataAnnotations;
using Application.Implementations.Dtos.Files;
using Application.Implementations.Dtos.Room;

namespace Application.Implementations.Dtos.Requests.RoomPatching;

public record PatchRoomRequest
{
    [Required]
    [Length(1, 64, ErrorMessage = "Длина имени должна быть от 1 до 64 символов включительно")]
    [RegularExpression("[a-zA-Zа-яА-Я0-9,\\.\\-#№\\(\\)]{0,64}", ErrorMessage = "В поле Name переданы недоступные символы")]
    public string Name { get; init; } = null!;

    [Length(1, 256, ErrorMessage = "Длина имени должна быть от 1 до 256 символов включительно")]
    public string? Description { get; init; }

    public RoomTypeDto Type { get; init; }
    public RoomLayoutDto Layout { get; init; }

    [Range(0, double.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? Seats { get; init; }

    [Range(0, double.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? ComputerSeats { get; init; }

    public FileMetadataDto? PdfRoomSchemeFileMetadata { get; init; }

    public FileMetadataDto? PhotoFileMetadata { get; init; }

    public RoomNetTypeDto NetType { get; init; }
    public bool HasConditioning { get; init; }

    [Length(1, 64, ErrorMessage = "Длина владельца должна быть от 1 до 64 символов включительно")]
    public string? Owner { get; init; }

    public RoomStatusDto RoomStatus { get; init; }

    [Length(1, 256, ErrorMessage = "Длина комментария должна быть от 1 до 256 символов включительно")]
    public string? Comment { get; init; }

    public DateTime? FixDeadline { get; init; }
    public bool AllowBooking { get; init; }
}