using Rooms.Core.Dtos.Files;

namespace Rooms.Core.Dtos.Room;

public record RoomAttachmentsDto(FileMetadataDto? PdfRoomScheme, FileMetadataDto? Photo);
