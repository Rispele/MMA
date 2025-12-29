using AutoFixture;
using Booking.Core.Interfaces.Dtos.BookingRequest;
using Booking.Core.Interfaces.Dtos.BookingRequest.Requests;
using Booking.Core.Interfaces.Services.BookingRequests;
using Booking.Domain.Propagated.BookingRequests;
using Booking.Tests.UnitTests;
using Commons.Tests.Helpers.SDK;
using FluentAssertions;
using Commons.Tests.Integration.Infrastructure;
using Commons.Tests.Integration.Infrastructure.ContainerBasedTests;

namespace Booking.Tests.IntegrationTests.Services;

[TestFixture]
public class BookingRequestServiceTests : ContainerTestBase
{
    [Inject] private readonly IBookingRequestService bookingRequestService = null!;

    [Inject] private readonly MmrSdk mmrSdk = null!;

    [Test]
    public async Task CreateBooking_ShouldSuccessfullyCreate()
    {
        var fixture = new Fixture().WithBookingRequestCustomization();

        var rooms = await Task.WhenAll(Enumerable.Range(0, 2)
            .Select(_ => mmrSdk.Rooms.CreateRoom(fixture.Create<string>())));
        var bookingTimes = rooms
            .Select(t => new BookingTimeDto()
            {
                RoomId = t.Id,
                Date = fixture.Create<DateOnly>(),
                TimeFrom = TimeOnly.Parse("16:00"),
                TimeTo = TimeOnly.Parse("20:00"),
                BookingFinishDate = null,
                Frequency = BookingFrequency.Undefined
            })
            .ToArray();
        var request = fixture.Build<CreateBookingRequestDto>()
            .With(t => t.BookingSchedule, bookingTimes)
            .Create();

        var created = await bookingRequestService.CreateBookingRequest(request, CancellationToken.None);

        var actual = await bookingRequestService.GetBookingRequestById(created.Id, CancellationToken.None);

        actual.Should().BeEquivalentTo(created, options => options.Excluding(o => o.CreatedAt));
        actual.CreatedAt.Should().BeCloseTo(created.CreatedAt, TimeSpan.FromMilliseconds(100));
    }
}