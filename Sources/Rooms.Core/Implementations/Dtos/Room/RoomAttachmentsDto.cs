using Rooms.Core.Implementations.Dtos.Files;

namespace Rooms.Core.Implementations.Dtos.Room;

public record RoomAttachmentsDto(FileMetadataDto? PdfRoomScheme, FileMetadataDto? Photo);
