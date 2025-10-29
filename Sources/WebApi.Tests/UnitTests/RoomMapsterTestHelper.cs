using Rooms.Core.Dtos.Files;
using Rooms.Core.Dtos.Room;
using WebApi.Models.Files;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Room;

namespace WebApi.Tests.UnitTests;

public static class RoomMapsterTestHelper
{
    private const int RoomId = 1;
    private const string RoomName = "Room";
    private const string RoomDescription = "Description";
    private const string RoomNumber = "Room Number";
    private const string RoomAddress = "Room Address";
    private const int Seats = 1;
    private const int ComputerSeats = 1;
    private const bool HasConditioning = true;
    private const string File1Name = "File1";
    private const string File2Name = "File2";
    private static readonly Guid File1Id = Guid.NewGuid();
    private static readonly Guid File2Id = Guid.NewGuid();
    private const string File1Bucket = "file1Bucket";
    private const string File2Bucket = "file2Bucket";
    private const string Owner = "Owner";
    private const int OperatorDepartmentId = 1;
    private const string DepartmentName = "Department";
    private const string Contacts = "Contacts";
    private const int OperatorId = 2;
    private const string OperatorName = "Operator";
    private const string OperatorUserId = "OperatorUserId";
    private static readonly DateTime FixDeadline = DateTime.Now;
    private const string FixComment = "Fix comment";
    private const bool AllowBooking = true;

    private const RoomTypeDto FromRoomType = RoomTypeDto.Computer;
    private const RoomLayoutDto FromRoomLayout = RoomLayoutDto.Amphitheater;
    private const RoomNetTypeDto FromRoomNetType = RoomNetTypeDto.WiredAndWireless;
    private const RoomStatusDto FromRoomStatus = RoomStatusDto.Ready;

    private const RoomTypeModel ToRoomType = RoomTypeModel.Computer;
    private const RoomLayoutModel ToRoomLayout = RoomLayoutModel.Amphitheater;
    private const RoomNetTypeModel ToRoomNetType = RoomNetTypeModel.WiredAndWireless;
    private const RoomStatusModel ToRoomStatus = RoomStatusModel.Ready;

    public static RoomDto CreateRoomDto()
    {
        return new RoomDto(
            RoomId,
            RoomName,
            RoomDescription,
            new ScheduleAddressDto(RoomNumber, RoomAddress),
            new RoomParametersDto(FromRoomType, FromRoomLayout, FromRoomNetType, Seats, ComputerSeats, HasConditioning),
            new RoomAttachmentsDto(
                new FileDescriptorDto(File1Name, new FileLocationDto(File1Id, File1Bucket)),
                new FileDescriptorDto(File2Name, new FileLocationDto(File2Id, File2Bucket))),
            Owner,
            new RoomOperatorDepartmentDto(
                OperatorDepartmentId,
                DepartmentName,
                Contacts,
                [
                    new RoomOperatorDto(OperatorId, OperatorName, OperatorUserId)
                ]),
            new RoomFixStatusDto(FromRoomStatus, FixDeadline, FixComment),
            AllowBooking,
            []);
    }

    public static RoomModel CreateRoomModel()
    {
        return new RoomModel
        {
            Id = RoomId,
            Name = RoomName,
            Description = RoomDescription,
            AllowBooking = AllowBooking,
            Attachments = new RoomAttachmentsModel(
                new FileDescriptorModel(File1Name, new FileLocationModel(File1Id, File1Bucket)),
                new FileDescriptorModel(File2Name, new FileLocationModel(File2Id, File2Bucket))),
            Owner = Owner,
            FixStatus = new RoomFixStatusModel(ToRoomStatus, FixDeadline, FixComment),
            OperatorDepartment = new RoomOperatorDepartmentModel(
                OperatorDepartmentId,
                DepartmentName,
                Contacts,
                [new RoomOperatorModel(OperatorId, OperatorName, OperatorUserId)]),
            Parameters = new RoomParametersModel
            {
                ComputerSeats = ComputerSeats,
                HasConditioning = HasConditioning,
                Layout = ToRoomLayout,
                NetType = ToRoomNetType,
                Seats = Seats,
                Type = ToRoomType
            },
            ScheduleAddress = new ScheduleAddressModel
            {
                Address = RoomAddress,
                RoomNumber = RoomNumber,
            }
        };
    }

    public static PatchRoomModel CreatePatchRoomModel()
    {
        return new PatchRoomModel
        {
            Name = RoomName,
            Description = RoomDescription,
            Type = ToRoomType,
            Layout = ToRoomLayout,
            NetType = ToRoomNetType,
            Seats = Seats,
            ComputerSeats = ComputerSeats,
            PdfRoomSchemeFile = new FileDescriptorModel(File1Name, new FileLocationModel(File1Id, File1Bucket)),
            PhotoFile = new FileDescriptorModel(File2Name, new FileLocationModel(File2Id, File2Bucket)),
            HasConditioning = HasConditioning,
            Owner = Owner,
            RoomStatus = ToRoomStatus,
            Comment = FixComment,
            FixDeadline = FixDeadline,
            AllowBooking = AllowBooking
        };
    }
}