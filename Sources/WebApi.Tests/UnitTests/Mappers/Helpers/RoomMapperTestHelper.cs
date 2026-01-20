using Commons.Core.Models.Filtering;
using Rooms.Core.Interfaces.Dtos.Files;
using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Interfaces.Dtos.Room.Fix;
using Rooms.Core.Interfaces.Dtos.Room.Parameters;
using Rooms.Core.Interfaces.Dtos.Room.Requests;
using Rooms.Domain.Propagated.Rooms;
using WebApi.Core.Models.Files;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.Filtering;
using WebApi.Core.Models.Requests.Rooms;
using WebApi.Core.Models.Room;
using WebApi.Core.Models.Room.Fix;
using WebApi.Core.Models.Room.Parameters;

namespace WebApi.Tests.UnitTests.Mappers.Helpers;

public static class RoomMapperTestHelper
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
    private const int OperatorDepartmentId = 1;
    private const string DepartmentName = "Department";
    private const string Contacts = "Contacts";
    private const int OperatorId = 2;
    private const string OperatorName = "Operator";
    private const string OperatorUserId = "OperatorUserId";
    private const string FixComment = "Fix comment";
    private const bool AllowBooking = true;

    private const RoomType RoomType = Rooms.Domain.Propagated.Rooms.RoomType.Computer;
    private const RoomLayout RoomLayout = Rooms.Domain.Propagated.Rooms.RoomLayout.Amphitheater;
    private const RoomNetType RoomNetType = Rooms.Domain.Propagated.Rooms.RoomNetType.WiredAndWireless;
    private const RoomStatus RoomStatus = Rooms.Domain.Propagated.Rooms.RoomStatus.Ready;


    private const int Page = 10;
    private const int PageSize = 11;

    private const SortDirectionDto AscendingSortDirectionDto = SortDirectionDto.Ascending;
    private const SortDirectionDto DescendingSortDirectionDto = SortDirectionDto.Descending;

    private const SortDirectionModel AscendingSortDirectionModel = SortDirectionModel.Ascending;
    private const SortDirectionModel DescendingSortDirectionModel = SortDirectionModel.Descending;
    private static readonly Guid File1Id = Guid.NewGuid();
    private static readonly Guid File2Id = Guid.NewGuid();
    private static readonly DateTime FixDeadline = DateTime.Now;

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
            FixInfo = new RoomFixStatusDto(RoomStatus, FixDeadline, FixComment),
            // OperatorDepartment = new RoomOperatorDepartmentModel(
            //     OperatorDepartmentId,
            //     DepartmentName,
            //     Contacts,
            //     [new RoomOperatorModel(OperatorId, OperatorName, OperatorUserId)]),
            Equipments = [],
            Parameters = new RoomParametersDto(RoomType, RoomLayout, RoomNetType, Seats, ComputerSeats, HasConditioning),
            ScheduleAddress = new ScheduleAddressDto
            {
                RoomNumber = RoomNumber, Address = RoomAddress, ScheduleRoomId = ScheduleAddressId
            }
        };
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
            FixInfo = new RoomFixStatusModel(RoomStatus, FixDeadline, FixComment),
            // OperatorDepartment = new RoomOperatorDepartmentModel(
            //     OperatorDepartmentId,
            //     DepartmentName,
            //     Contacts,
            //     [new RoomOperatorModel(OperatorId, OperatorName, OperatorUserId)]),
            Parameters = new RoomParametersModel
            {
                ComputerSeats = ComputerSeats,
                HasConditioning = HasConditioning,
                Layout = RoomLayout,
                NetType = RoomNetType,
                Seats = Seats,
                Type = RoomType
            },
            ScheduleAddress = new ScheduleAddressModel
            {
                Address = RoomAddress,
                RoomNumber = RoomNumber,
                ScheduleRoomId = 0,
            }
        };
    }

    public static CreateRoomModel CreateCreateRoomModel()
    {
        return new CreateRoomModel
        {
            Name = RoomName,
            Description = RoomDescription,
            Type = RoomType,
            Layout = RoomLayout,
            NetType = RoomNetType,
            Seats = Seats,
            ComputerSeats = ComputerSeats,
            PdfRoomSchemeFile = new FileDescriptorModel(File1Name, new FileLocationModel(File1Id, File1Bucket)),
            PhotoFile = new FileDescriptorModel(File2Name, new FileLocationModel(File2Id, File2Bucket)),
            HasConditioning = HasConditioning,
            Owner = Owner,
            RoomStatus = RoomStatus,
            Comment = FixComment,
            FixDeadline = FixDeadline,
            AllowBooking = AllowBooking
        };
    }

    public static CreateRoomDto CreateCreateRoomDto()
    {
        return new CreateRoomDto
        {
            Name = RoomName,
            Description = RoomDescription,
            Type = RoomType,
            Layout = RoomLayout,
            NetType = RoomNetType,
            Seats = Seats,
            ComputerSeats = ComputerSeats,
            PdfRoomSchemeFile = new FileDescriptorDto(File1Name, new FileLocationDto(File1Id, File1Bucket)),
            PhotoFile = new FileDescriptorDto(File2Name, new FileLocationDto(File2Id, File2Bucket)),
            HasConditioning = HasConditioning,
            Owner = Owner,
            RoomStatus = RoomStatus,
            Comment = FixComment,
            FixDeadline = FixDeadline,
            AllowBooking = AllowBooking
        };
    }


    public static PatchRoomModel CreatePatchRoomModel()
    {
        return new PatchRoomModel
        {
            Name = RoomName,
            Description = RoomDescription,
            Type = RoomType,
            ScheduleAddress = new ScheduleAddressModel
            {
                Address = RoomAddress,
                RoomNumber = RoomNumber,
                ScheduleRoomId = 0,
            },
            Layout = RoomLayout,
            NetType = RoomNetType,
            Seats = Seats,
            ComputerSeats = ComputerSeats,
            PdfRoomSchemeFile = new FileDescriptorModel(File1Name, new FileLocationModel(File1Id, File1Bucket)),
            PhotoFile = new FileDescriptorModel(File2Name, new FileLocationModel(File2Id, File2Bucket)),
            HasConditioning = HasConditioning,
            Owner = Owner,
            RoomStatus = RoomStatus,
            Comment = FixComment,
            FixDeadline = FixDeadline,
            AllowBooking = AllowBooking
        };
    }

    public static PatchRoomDto CreatePatchRoomDto()
    {
        return new PatchRoomDto
        {
            Name = RoomName,
            Description = RoomDescription,
            ScheduleAddress = new ScheduleAddressPatchDto(RoomNumber, RoomAddress),
            Type = RoomType,
            Layout = RoomLayout,
            NetType = RoomNetType,
            Seats = Seats,
            ComputerSeats = ComputerSeats,
            PdfRoomSchemeFile = new FileDescriptorDto(File1Name, new FileLocationDto(File1Id, File1Bucket)),
            PhotoFile = new FileDescriptorDto(File2Name, new FileLocationDto(File2Id, File2Bucket)),
            HasConditioning = HasConditioning,
            Owner = Owner,
            RoomStatus = RoomStatus,
            Comment = FixComment,
            FixDeadline = FixDeadline,
            AllowBooking = AllowBooking
        };
    }

    public static GetRoomsRequestDto CreateGetRoomsRequestDto()
    {
        return new GetRoomsRequestDto
        {
            BatchNumber = Page - 1,
            BatchSize = PageSize,
            Filter = new RoomsFilterDto
            {
                Name = CreateFilterParameterDto(AscendingSortDirectionDto, RoomName),
                Description = CreateFilterParameterDto(AscendingSortDirectionDto, RoomDescription),
                RoomTypes = CreateFilterMultiParameterDto(AscendingSortDirectionDto, RoomType),
                RoomLayout = CreateFilterMultiParameterDto(AscendingSortDirectionDto, RoomLayout),
                Seats = CreateFilterParameterDto(AscendingSortDirectionDto, Seats),
                ComputerSeats = CreateFilterParameterDto(AscendingSortDirectionDto, ComputerSeats),
                NetTypes = CreateFilterMultiParameterDto(AscendingSortDirectionDto, RoomNetType),
                Conditioning = CreateFilterParameterDto(AscendingSortDirectionDto, HasConditioning),
                OperatorDepartments = CreateFilterMultiParameterDto(AscendingSortDirectionDto, OperatorDepartmentId),
                Operator = CreateFilterParameterDto(AscendingSortDirectionDto, OperatorName),
                Owner = CreateFilterParameterDto(AscendingSortDirectionDto, Owner),
                RoomStatuses = CreateFilterMultiParameterDto(AscendingSortDirectionDto, RoomStatus),
                FixDeadline = CreateFilterParameterDto(AscendingSortDirectionDto, FixDeadline),
                Comment = CreateFilterParameterDto(AscendingSortDirectionDto, FixComment)
                // AllowBooking = CreateFilterParameterDto(DescendingSortDirectionDto, AllowBooking),
            }
        };
    }

    public static GetRequest<RoomsFilterModel> CreateGetRoomsModel()
    {
        return new GetRequest<RoomsFilterModel>
        {
            Page = Page,
            PageSize = PageSize,
            Filter = new RoomsFilterModel
            {
                Name = CreateFilterParameterModel(AscendingSortDirectionModel, RoomName),
                Description = CreateFilterParameterModel(AscendingSortDirectionModel, RoomDescription),
                RoomTypes = CreateFilterMultiParameterModel(AscendingSortDirectionModel, RoomType),
                RoomLayout = CreateFilterMultiParameterModel(AscendingSortDirectionModel, RoomLayout),
                Seats = CreateFilterParameterModel(AscendingSortDirectionModel, Seats),
                ComputerSeats = CreateFilterParameterModel(AscendingSortDirectionModel, ComputerSeats),
                NetTypes = CreateFilterMultiParameterModel(AscendingSortDirectionModel, RoomNetType),
                Conditioning = CreateFilterParameterModel(AscendingSortDirectionModel, HasConditioning),
                OperatorDepartments = CreateFilterMultiParameterModel(AscendingSortDirectionModel, OperatorDepartmentId),
                Operator = CreateFilterParameterModel(AscendingSortDirectionModel, OperatorName),
                Owner = CreateFilterParameterModel(AscendingSortDirectionModel, Owner),
                RoomStatuses = CreateFilterMultiParameterModel(AscendingSortDirectionModel, RoomStatus),
                FixDeadline = CreateFilterParameterModel(AscendingSortDirectionModel, FixDeadline),
                Comment = CreateFilterParameterModel(AscendingSortDirectionModel, FixComment)
            }
        };
    }

    private static FilterParameterModel<TValue> CreateFilterParameterModel<TValue>(SortDirectionModel sortDirection, TValue value)
    {
        return new FilterParameterModel<TValue>
        {
            SortDirection = sortDirection,
            Value = value
        };
    }

    private static FilterMultiParameterModel<TValue> CreateFilterMultiParameterModel<TValue>(
        SortDirectionModel sortDirection,
        TValue value,
        params TValue[] values)
    {
        var values1 = new[] { value }.Concat(values).ToArray();

        return new FilterMultiParameterModel<TValue>
        {
            SortDirection = sortDirection,
            Values = values1
        };
    }

    private static FilterParameterDto<TValue> CreateFilterParameterDto<TValue>(SortDirectionDto sortDirection, TValue value)
    {
        return new FilterParameterDto<TValue>(value, sortDirection);
    }

    private static FilterMultiParameterDto<TValue> CreateFilterMultiParameterDto<TValue>(
        SortDirectionDto sortDirection,
        TValue value,
        params TValue[] values)
    {
        var values1 = new[] { value }.Concat(values).ToArray();

        return new FilterMultiParameterDto<TValue>(values1, sortDirection);
    }
}