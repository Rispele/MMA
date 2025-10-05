using Application.Clients.Dtos.Files;

namespace Application.Clients.Dtos.Room;

public record RoomAttachmentsDto(FileMetadataDto? PdfRoomScheme, FileMetadataDto? Photo);
