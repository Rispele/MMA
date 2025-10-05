using Application.Implementations.Dtos.Files;

namespace Application.Implementations.Dtos.Room;

public record RoomAttachmentsDto(FileMetadataDto? PdfRoomScheme, FileMetadataDto? Photo);
