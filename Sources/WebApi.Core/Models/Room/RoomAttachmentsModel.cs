using WebApi.Core.Models.Files;

namespace WebApi.Core.Models.Room;

public record RoomAttachmentsModel(FileDescriptorModel? PdfRoomScheme, FileDescriptorModel? Photo);