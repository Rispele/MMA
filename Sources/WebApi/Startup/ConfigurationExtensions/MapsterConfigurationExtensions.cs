using Mapster;
using Rooms.Core.Dtos.Room;
using WebApi.Models.Requests.Rooms;

namespace WebApi.Startup.ConfigurationExtensions;

public static class MapsterConfigurationExtensions
{
    public static TypeAdapterConfig ConfigureMapster(this TypeAdapterConfig config)
    {
        config
            .NewConfig<RoomDto, PatchRoomModel>()
            .Map(t => t.Type, t => t.Parameters.Type)
            .Map(t => t.NetType, t => t.Parameters.NetType)
            .Map(t => t.Layout, t => t.Parameters.Layout)
            .Map(t => t.Seats, t => t.Parameters.Seats)
            .Map(t => t.ComputerSeats, t => t.Parameters.ComputerSeats)
            .Map(t => t.HasConditioning, t => t.Parameters.HasConditioning)
            .Map(t => t.PdfRoomSchemeFile, t => t.Attachments.PdfRoomScheme)
            .Map(t => t.PhotoFile, t => t.Attachments.Photo)
            .Map(t => t.Comment, t => t.FixStatus.Comment)
            .Map(t => t.FixDeadline, t => t.FixStatus.FixDeadline)
            .Map(t => t.RoomStatus, t => t.FixStatus.Status);
            
        
        return config;
    }
}