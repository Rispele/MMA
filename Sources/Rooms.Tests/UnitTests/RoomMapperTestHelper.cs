using Rooms.Core.Dtos.Files;
using Rooms.Core.Dtos.Room;
using Rooms.Core.Dtos.Room.Fix;
using Rooms.Core.Dtos.Room.Parameters;
using Rooms.Domain.Models.File;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Tests.UnitTests;

public static class RoomMapperTestHelper
{
    private const int RoomId = 1;
    private const string RoomName = "Room";
    private const string RoomDescription = "Description";
    private const string RoomNumber = "Room Number";
    private const string RoomAddress = "Room Address";
    private const int Seats = 10;
    private const int ComputerSeats = 20;
    private const bool HasConditioning = true;
    private const string File1Name = "File1";
    private const string File2Name = "File2";
    private static readonly Guid File1Id = Guid.NewGuid();
    private static readonly Guid File2Id = Guid.NewGuid();
    private const string File1Bucket = "file1Bucket";
    private const string File2Bucket = "file2Bucket";
    private const string Owner = "Owner";
    // private const int OperatorDepartmentId = 1;
    // private const string DepartmentName = "Department";
    // private const string Contacts = "Contacts";
    // private const int OperatorId = 2;
    // private const string OperatorName = "Operator";
    // private const string OperatorUserId = "OperatorUserId";
    private static readonly DateTime FixDeadline = DateTime.Now;
    private const string FixComment = "Fix comment";
    private const bool AllowBooking = true;

    private const RoomTypeDto ToRoomType = RoomTypeDto.Computer;
    private const RoomLayoutDto ToRoomLayout = RoomLayoutDto.Amphitheater;
    private const RoomNetTypeDto ToRoomNetType = RoomNetTypeDto.WiredAndWireless;
    private const RoomStatusDto ToRoomStatus = RoomStatusDto.Ready;

    private const RoomType FromRoomType = RoomType.Computer;
    private const RoomLayout FromRoomLayout = RoomLayout.Amphitheater;
    private const RoomNetType FromRoomNetType = RoomNetType.WiredAndWireless;
    private const RoomStatus FromRoomStatus = RoomStatus.Ready;

    public static RoomDto CreateRoomDto()
    {
        return new RoomDto
        {
            Id = RoomId,
            Name = RoomName,
            Description = RoomDescription,
            AllowBooking = AllowBooking,
            Attachments = new RoomAttachmentsDto(
                new FileDescriptorDto(File1Name, new FileLocationDto(File1Id, File1Bucket)),
                new FileDescriptorDto(File2Name, new FileLocationDto(File2Id, File2Bucket))),
            Owner = Owner,
            FixInfo = new RoomFixStatusDto(ToRoomStatus, FixDeadline, FixComment),
            Equipments = [],
            Parameters = new RoomParametersDto(ToRoomType, ToRoomLayout, ToRoomNetType, Seats, ComputerSeats, HasConditioning),
            ScheduleAddress = new ScheduleAddressDto(RoomNumber, RoomAddress),
        };
    }

    public static Room CreateRoom()
    {
        var room = new Room(
            RoomName,
            RoomDescription,
            new RoomParameters
            {
                ComputerSeats = ComputerSeats,
                HasConditioning = HasConditioning,
                Layout = FromRoomLayout,
                NetType = FromRoomNetType,
                Seats = Seats,
                Type = FromRoomType
            },
            new RoomAttachments
            {
                PdfRoomScheme = new FileDescriptor(File1Name, new FileLocation(File1Id, File1Bucket)),
                Photo = new FileDescriptor(File2Name, new FileLocation(File2Id, File2Bucket))
            },
            Owner,
            new RoomFixInfo
            {
                Comment = FixComment,
                FixDeadline = FixDeadline,
                Status = FromRoomStatus,
            },
            AllowBooking);
        
        room.SetId(RoomId);
        room.SetScheduleAddress(RoomNumber, RoomAddress);
        
        return room;
    }
}