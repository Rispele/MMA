using Rooms.Core.Interfaces.Dtos.Files;
using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Interfaces.Dtos.Room.Fix;
using Rooms.Core.Interfaces.Dtos.Room.Parameters;
using Rooms.Domain.Models.File;
using Rooms.Domain.Models.Rooms;
using Rooms.Domain.Models.Rooms.Fix;
using Rooms.Domain.Models.Rooms.Parameters;
using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Tests.UnitTests;

internal static class RoomTestHelper
{
    private const int RoomId = 1;
    private const string RoomName = "Room";
    private const string RoomDescription = "Description";
    private const string RoomNumber = "Room Number";
    private const string RoomAddress = "Room Address";
    private const int ScheduleAddressId = 1;
    private const int Seats = 10;
    private const int ComputerSeats = 20;
    private const bool HasConditioning = true;
    private const string File1Name = "File1";
    private const string File2Name = "File2";
    private const string File1Bucket = "file1Bucket";
    private const string File2Bucket = "file2Bucket";
    private const string Owner = "Owner";
    private const string FixComment = "Fix comment";
    private const bool AllowBooking = true;

    private const RoomType RoomType = Domain.Propagated.Rooms.RoomType.Computer;
    private const RoomLayout RoomLayout = Domain.Propagated.Rooms.RoomLayout.Amphitheater;
    private const RoomNetType RoomNetType = Domain.Propagated.Rooms.RoomNetType.WiredAndWireless;
    private const RoomStatus RoomStatus = Domain.Propagated.Rooms.RoomStatus.Ready;

    private static readonly Guid File1Id = Guid.NewGuid();

    private static readonly Guid File2Id = Guid.NewGuid();

    // private const int OperatorDepartmentId = 1;
    // private const string DepartmentName = "Department";
    // private const string Contacts = "Contacts";
    // private const int OperatorId = 2;
    // private const string OperatorName = "Operator";
    // private const string OperatorUserId = "OperatorUserId";
    private static readonly DateTime FixDeadline = DateTime.Now;

    public static RoomDto CreateRoomDto(int roomId = RoomId, int? operatorDepartmentId = null)
    {
        return new RoomDto
        {
            Id = roomId,
            Name = RoomName,
            Description = RoomDescription,
            AllowBooking = AllowBooking,
            Attachments = new RoomAttachmentsDto(
                new FileDescriptorDto(File1Name, new FileLocationDto(File1Id, File1Bucket)),
                new FileDescriptorDto(File2Name, new FileLocationDto(File2Id, File2Bucket))),
            Owner = Owner,
            FixInfo = new RoomFixStatusDto(RoomStatus, FixDeadline, FixComment),
            Equipments = [],
            Parameters = new RoomParametersDto(RoomType, RoomLayout, RoomNetType, Seats, ComputerSeats, HasConditioning),
            ScheduleAddress = new ScheduleAddressDto
            {
                RoomNumber = RoomNumber, Address = RoomAddress, ScheduleRoomId = ScheduleAddressId
            },
            OperatorDepartmentId = operatorDepartmentId
        };
    }

    public static Room CreateRoom()
    {
        var room = new Room(
            RoomId,
            RoomName,
            RoomDescription,
            new RoomParameters
            {
                ComputerSeats = ComputerSeats,
                HasConditioning = HasConditioning,
                Layout = RoomLayout,
                NetType = RoomNetType,
                Seats = Seats,
                Type = RoomType
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
                Status = RoomStatus
            },
            AllowBooking);

        room.SetScheduleAddress(RoomNumber, RoomAddress, ScheduleAddressId);

        return room;
    }
}