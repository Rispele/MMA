using AutoFixture;
using Booking.Core.Interfaces.Dtos.BookingRequest.Requests;
using Booking.Core.Interfaces.Services.BookingRequests;
using Booking.Tests.UnitTests;
using Commons.Tests.Helpers.SDK;
using FluentAssertions;
using IntegrationTestInfrastructure;
using IntegrationTestInfrastructure.ContainerBasedTests;

namespace Booking.Tests.IntegrationTests.Services;

[TestFixture]
public class BookingRequestServiceTests : ContainerTestBase
{
    [Inject]
    private readonly IBookingRequestService bookingRequestService = null!;

    [Inject]
    private readonly MmrSdk mmrSdk = null!; 
    
    [Test]
    public async Task CreateBooking_ShouldSuccessfullyCreate()
    {
        var fixture = new Fixture().WithBookingRequestCustomization();

        var rooms = await Task.WhenAll(Enumerable.Range(0, 2).Select(_ => mmrSdk.Rooms.CreateRoom(fixture.Create<string>())));
        var request = fixture.Build<CreateBookingRequestDto>()
            .With(t => t.RoomIds, rooms.Select(r => r.Id).ToArray)
            .With(t => t.CreatedAt, DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.Next(6) - 3))
            .Create();
        
        var created = await bookingRequestService.CreateBookingRequest(request, CancellationToken.None);
        
        var actual = await bookingRequestService.GetBookingRequestById(created.Id, CancellationToken.None);
        
        actual.Should().BeEquivalentTo(created, options => options.Excluding(o => o.CreatedAt));
        actual.CreatedAt.Should().BeCloseTo(created.CreatedAt, TimeSpan.FromMilliseconds(100));
    }
}