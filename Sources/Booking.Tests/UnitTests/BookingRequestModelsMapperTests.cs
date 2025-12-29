using AutoFixture;
using Booking.Core.Interfaces.Dtos.BookingRequest;
using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;
using Booking.Domain.Models.BookingRequests;
using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using Commons.Tests.Helpers.Customizations;
using FluentAssertions;
using BookingRequestDtoMapper = Booking.Core.Services.BookingRequests.Mappers.BookingRequestDtoMapper;

namespace Booking.Tests.UnitTests;

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
        var mapped = BookingRequestDtoMapper.MapRoomEventCoordinatorFromDto(dto);

        AssertCoordinators(mapped, dto);
    }

    [TestCase(typeof(OtherRoomEventCoordinator))]
    [TestCase(typeof(ScientificRoomEventCoordinator))]
    [TestCase(typeof(StudentRoomEventCoordinator))]
    public void Map_RoomEventCoordinatorModel_To_Dto_ShouldCorrectlyMap(Type coordinatorType)
    {
        var fixture = new Fixture()
            .WithCoordinatorCustomization(coordinatorType)
            .WithDateOnlyCustomization();

        var model = fixture.Create<IRoomEventCoordinator>();
        var mapped = BookingRequestDtoMapper.MapRoomEventCoordinatorToDto(model);

        AssertCoordinators(mapped, model);
    }


    [Test]
    public void Map_BookingRequest_To_Dto_ShouldCorrectlyMap()
    {
        var fixture = new Fixture().WithBookingRequestCustomization();
        var actual = new BookingRequest(
            fixture.Create<int>(),
            fixture.Create<BookingCreator>(),
            fixture.Create<string>(),
            fixture.Create<int>(),
            fixture.Create<bool>(),
            fixture.Create<string>(),
            fixture.Create<IRoomEventCoordinator>(),
            fixture.Create<string>(),
            fixture.Create<BookingTime[]>());

        var expected = BookingRequestDtoMapper.MapBookingRequestToDto(actual);

        expected.Reason.Should().Be(actual.Reason);
        expected.ParticipantsCount.Should().Be(actual.ParticipantsCount);
        expected.TechEmployeeRequired.Should().Be(actual.TechEmployeeRequired);
        expected.EventHostFullName.Should().Be(actual.EventHostFullName);
        expected.CreatedAt.Should().Be(actual.CreatedAt);
        expected.EventName.Should().Be(actual.EventName);
        expected.Status.Should().Be(actual.Status);
        expected.ModeratorComment.Should().Be(actual.ModeratorComment);
        expected.BookingScheduleStatus.Should().Be(actual.BookingScheduleStatus);

        AssertCreators(expected.Creator, actual.Creator);
        AssertCoordinators(expected.RoomEventCoordinator, actual.RoomEventCoordinator);
        AssertBookingSchedule(expected.BookingSchedule, actual.BookingSchedule);
    }

    # region Assertions

    private static void AssertBookingSchedule(IEnumerable<BookingTimeDto> dto, IEnumerable<BookingTime> mapped)
    {
        AssertBookingSchedule(mapped, dto);
    }

    private static void AssertCoordinators(IRoomEventCoordinatorDto dto, IRoomEventCoordinator mapped)
    {
        AssertCoordinators(mapped, dto);
    }

    private static void AssertCreators(BookingCreatorDto dto, BookingCreator mapped)
    {
        AssertCreators(mapped, dto);
    }

    private static void AssertBookingSchedule(IEnumerable<BookingTime> mapped, IEnumerable<BookingTimeDto> dto)
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

    private static void AssertCoordinators(IRoomEventCoordinator mapped, IRoomEventCoordinatorDto dto)
    {
        switch (mapped)
        {
            case OtherRoomEventCoordinator:
                dto.Should().BeOfType<OtherRoomEventCoordinatorDto>();
                break;
            case ScientificRoomEventCoordinator scientificRoomEventCoordinatorModel:
                var coordinator = dto.Should().BeOfType<ScientificRoomEventCoordinatorDto>().Subject;
                scientificRoomEventCoordinatorModel.CoordinatorDepartmentId.Should().Be(coordinator.CoordinatorDepartmentId);
                scientificRoomEventCoordinatorModel.CoordinatorId.Should().Be(coordinator.CoordinatorId);
                break;
            case StudentRoomEventCoordinator:
                dto.Should().BeOfType<StudentRoomEventCoordinatorDto>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void AssertCreators(BookingCreator mapped, BookingCreatorDto dto)
    {
        mapped.Email.Should().Be(dto.Email);
        mapped.FullName.Should().Be(dto.FullName);
        mapped.PhoneNumber.Should().Be(dto.PhoneNumber);
    }

    #endregion
}