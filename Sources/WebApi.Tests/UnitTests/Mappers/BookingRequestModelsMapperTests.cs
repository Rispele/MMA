using AutoFixture;
using Booking.Core.Interfaces.Dtos.BookingRequest;
using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;
using Commons.Tests.Helpers.Customizations;
using FluentAssertions;
using WebApi.Core.ModelConverters;
using WebApi.Core.Models.BookingRequest;
using WebApi.Core.Models.BookingRequest.RoomEventCoordinator;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.BookingRequests;
using WebApi.Tests.UnitTests.Mappers.Helpers;

namespace WebApi.Tests.UnitTests.Mappers;

[TestFixture]
public class BookingRequestModelsMapperTests
{
    [TestCase(typeof(OtherRoomEventCoordinatorDto))]
    [TestCase(typeof(ScientificRoomEventCoordinatorDto))]
    [TestCase(typeof(StudentRoomEventCoordinatorDto))]
    public void Map_RoomEventCoordinatorDto_To_Model_ShouldCorrectlyMap(Type coordinatorType)
    {
        var fixture = new Fixture()
            .WithCoordinatorDtoCustomization(coordinatorType)
            .WithDateOnlyCustomization();

        var dto = fixture.Create<IRoomEventCoordinatorDto>();
        var mapped = BookingRequestModelsMapper.MapRoomEventCoordinatorFromDto(dto);

        AssertCoordinators(mapped, dto);
    }

    [TestCase(typeof(OtherRoomEventCoordinatorModel))]
    [TestCase(typeof(ScientificRoomEventCoordinatorModel))]
    [TestCase(typeof(StudentRoomEventCoordinatorModel))]
    public void Map_RoomEventCoordinatorModel_To_Dto_ShouldCorrectlyMap(Type coordinatorType)
    {
        var fixture = new Fixture()
            .WithCoordinatorModelCustomization(coordinatorType)
            .WithDateOnlyCustomization();

        var model = fixture.Create<IRoomEventCoordinatorModel>();
        var mapped = BookingRequestModelsMapper.MapRoomEventCoordinatorToDto(model);

        AssertCoordinators(mapped, model);
    }

    [Test]
    public void Map_BookingRequestDto_To_BookingRequestModel_ShouldCorrectlyMap()
    {
        var fixture = new Fixture().WithBookingRequestCustomization();
        var dto = fixture.Create<BookingRequestDto>();

        var mapped = BookingRequestModelsMapper.MapBookingRequestToModel(dto);

        mapped.Id.Should().Be(dto.Id);
        mapped.Reason.Should().Be(dto.Reason);
        mapped.ParticipantsCount.Should().Be(dto.ParticipantsCount);
        mapped.TechEmployeeRequired.Should().Be(dto.TechEmployeeRequired);
        mapped.EventHost.Name.Should().Be(dto.EventHost.Name);
        mapped.EventHost.Id.Should().Be(dto.EventHost.Id);
        mapped.CreatedAt.Should().Be(dto.CreatedAt);
        mapped.EventName.Should().Be(dto.EventName);
        mapped.Status.Should().Be(dto.Status);
        mapped.ModeratorComment.Should().Be(dto.ModeratorComment);
        mapped.BookingScheduleStatus.Should().Be(dto.BookingScheduleStatus);

        AssertCreators(mapped.Creator, dto.Creator);
        AssertCoordinators(mapped.RoomEventCoordinator, dto.RoomEventCoordinator);
        AssertBookingSchedule(mapped.BookingSchedule, dto.BookingSchedule);
    }

    [Test]
    public void Map_BookingRequestDto_To_PatchModel_ShouldCorrectlyMap()
    {
        var fixture = new Fixture().WithBookingRequestCustomization();
        var dto = fixture.Create<BookingRequestDto>();

        var mapped = BookingRequestModelsMapper.MapBookingRequestToPatchModel(dto);

        mapped.Reason.Should().Be(dto.Reason);
        mapped.ParticipantsCount.Should().Be(dto.ParticipantsCount);
        mapped.TechEmployeeRequired.Should().Be(dto.TechEmployeeRequired);
        mapped.EventName.Should().Be(dto.EventName);
        mapped.EventHost.Name.Should().Be(dto.EventHost.Name);
        mapped.EventHost.Id.Should().Be(dto.EventHost.Id);

        AssertCreators(mapped.Creator, dto.Creator);
        AssertCoordinators(mapped.RoomEventCoordinator, dto.RoomEventCoordinator);
        AssertBookingSchedule(mapped.BookingSchedule, dto.BookingSchedule);
    }

    [Test]
    public void Map_PatchModel_To_BookingRequestDto_ShouldCorrectlyMap()
    {
        var fixture = new Fixture().WithBookingRequestCustomization();
        var patchModel = fixture.Create<PatchBookingRequestModel>();

        var mapped = BookingRequestModelsMapper.MapPatchBookingRequestFromModel(patchModel);

        mapped.Reason.Should().Be(patchModel.Reason);
        mapped.ParticipantsCount.Should().Be(patchModel.ParticipantsCount);
        mapped.TechEmployeeRequired.Should().Be(patchModel.TechEmployeeRequired);
        mapped.EventHost.Name.Should().Be(patchModel.EventHost.Name);
        mapped.EventHost.Id.Should().Be(patchModel.EventHost.Id);
        mapped.EventName.Should().Be(patchModel.EventName);

        AssertCreators(mapped.Creator, patchModel.Creator);
        AssertCoordinators(mapped.RoomEventCoordinator, patchModel.RoomEventCoordinator);
        AssertBookingSchedule(mapped.BookingSchedule, patchModel.BookingSchedule);
    }

    [Test]
    public void Map_GetBookingRequestModel_To_GetBookingRequestDto_ShouldCorrectlyMap()
    {
        var fixture = new Fixture().WithBookingRequestCustomization();
        var initial = fixture.Create<GetRequest<BookingRequestsFilterModel>>();

        var mapped = BookingRequestModelsMapper.MapGetBookingRequestFromModel(initial);

        mapped.BatchNumber.Should().Be(initial.Page - 1);
        mapped.BatchSize.Should().Be(initial.PageSize);

        var mappedFilter = mapped.Filter;
        var initialFilter = initial.Filter;

        AssertionHelper.AssertNullablesEqual(
            mappedFilter,
            initialFilter,
            (actual, expected) =>
            {
                AssertionHelper.AssertFilterParameter(actual.BookingScheduleStatus, expected.BookingScheduleStatus);
                AssertionHelper.AssertFilterParameter(actual.Rooms, expected.Rooms);
                AssertionHelper.AssertFilterParameter(actual.Status, expected.Status);
                AssertionHelper.AssertFilterParameter(actual.CreatedAt, expected.CreatedAt);
                AssertionHelper.AssertFilterParameter(actual.EventName, expected.EventName);
            });
    }

    [Test]
    public void Map_CreateBookingRequestModel_To_CreateBookingRequestDto_ShouldCorrectlyMap()
    {
        var fixture = new Fixture().WithBookingRequestCustomization();
        var expected = fixture.Create<CreateBookingRequestModel>();

        var actual = BookingRequestModelsMapper.MapCreateBookingRequestFromModel(expected);

        actual.Reason.Should().Be(expected.Reason);
        actual.ParticipantsCount.Should().Be(expected.ParticipantsCount);
        actual.TechEmployeeRequired.Should().Be(expected.TechEmployeeRequired);
        actual.EventHost.Name.Should().Be(expected.EventHost.Name);
        actual.EventHost.Id.Should().Be(expected.EventHost.Id);
        actual.EventName.Should().Be(expected.EventName);

        AssertCreators(actual.Creator, expected.Creator);
        AssertCoordinators(actual.RoomEventCoordinator, expected.RoomEventCoordinator);
        AssertBookingSchedule(actual.BookingSchedule, expected.BookingSchedule);
    }

    # region Assertions

    private static void AssertBookingSchedule(IEnumerable<BookingTimeDto> dto, IEnumerable<BookingTimeModel> mapped)
    {
        AssertBookingSchedule(mapped, dto);
    }

    private static void AssertCoordinators(IRoomEventCoordinatorDto dto, IRoomEventCoordinatorModel mapped)
    {
        AssertCoordinators(mapped, dto);
    }

    private static void AssertCreators(BookingCreatorDto dto, BookingCreatorModel mapped)
    {
        AssertCreators(mapped, dto);
    }

    private static void AssertBookingSchedule(IEnumerable<BookingTimeModel> mapped, IEnumerable<BookingTimeDto> dto)
    {
        var mappedArr = mapped.ToArray();
        var dtoArr = dto.ToArray();

        mappedArr.Should().HaveSameCount(dtoArr);
        foreach (var (bookingTimeModel, bookingTimeDto) in mappedArr.Zip(dtoArr))
        {
            bookingTimeModel.BookingFinishDate.Should().Be(bookingTimeDto.BookingFinishDate);
            bookingTimeModel.Date.Should().Be(bookingTimeDto.Date);
            bookingTimeModel.RoomId.Should().Be(bookingTimeDto.RoomId);
            bookingTimeModel.Frequency.Should().Be(bookingTimeDto.Frequency);
            bookingTimeModel.TimeFrom.Should().Be(bookingTimeDto.TimeFrom);
            bookingTimeModel.TimeTo.Should().Be(bookingTimeDto.TimeTo);
        }
    }

    private static void AssertCoordinators(IRoomEventCoordinatorModel mapped, IRoomEventCoordinatorDto dto)
    {
        switch (mapped)
        {
            case OtherRoomEventCoordinatorModel:
                dto.Should().BeOfType<OtherRoomEventCoordinatorDto>();
                break;
            case ScientificRoomEventCoordinatorModel scientificRoomEventCoordinatorModel:
                var coordinator = dto.Should().BeOfType<ScientificRoomEventCoordinatorDto>().Subject;
                scientificRoomEventCoordinatorModel.CoordinatorDepartmentId.Should().Be(coordinator.CoordinatorDepartmentId);
                scientificRoomEventCoordinatorModel.CoordinatorId.Should().Be(coordinator.CoordinatorId);
                break;
            case StudentRoomEventCoordinatorModel:
                dto.Should().BeOfType<StudentRoomEventCoordinatorDto>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void AssertCreators(BookingCreatorModel mapped, BookingCreatorDto dto)
    {
        mapped.Email.Should().Be(dto.Email);
        mapped.FullName.Should().Be(dto.FullName);
        mapped.PhoneNumber.Should().Be(dto.PhoneNumber);
    }

    #endregion
}