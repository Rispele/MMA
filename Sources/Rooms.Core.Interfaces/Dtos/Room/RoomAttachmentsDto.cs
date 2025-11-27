using Rooms.Core.Interfaces.Dtos.Files;

namespace Rooms.Core.Interfaces.Dtos.Room;

public record RoomAttachmentsDto(FileDescriptorDto? PdfRoomScheme, FileDescriptorDto? Photo);