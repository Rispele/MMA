using Commons.ExternalClients.Booking;
using Commons.ExternalClients.Booking.Models;
using FluentAssertions;

namespace Commons.ExternalClients.Tests;

[Parallelizable(ParallelScope.None)]
public class BookingClientTests
{
    [Test]
    public async Task GetFreeRooms_ShouldNotThrow_And_NotEmpty()
    {
        var client = SetupClient();

        var today = DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(1));
        var from = TimeOnly.Parse("16:00:00");
        var to = TimeOnly.Parse("20:00:00");
        var request = new GetFreeRoomsRequest(today, from, to);

        var response = await client.GetRoomsAvailableForBooking(request, CancellationToken.None);
        response.Result.Should().NotBeEmpty();
    }

    [Test]
    public async Task BookFirstAvailableRoom_ShouldSucceed_And_ReturnBookingEvent()
    {
        var client = SetupClient();

        var bookingResponse = await BookRoom(client);

        bookingResponse.IsOk.Should().BeTrue();
    }

    [Test]
    public async Task DeclineBooking_ShouldSucceed()
    {
        var client = SetupClient();

        var bookingResponse = await BookRoom(client);

        bookingResponse.IsOk.Should().BeTrue();

        var response = await client.DeclineBooking(bookingResponse.Result, CancellationToken.None);
        response.IsOk.Should().BeTrue();
    }

    [Test]
    public async Task ConfirmBooking_ShouldSucceed()
    {
        var client = SetupClient();

        var bookingResponse = await BookRoom(client);

        bookingResponse.IsOk.Should().BeTrue();

        var response = await client.ConfirmBooking(bookingResponse.Result, CancellationToken.None);
        response.IsOk.Should().BeTrue();
    }

    private static async Task<BookingResponse<long>> BookRoom(IBookingClient client)
    {
        var today = DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(1));
        var from = TimeOnly.Parse("16:00:00");
        var to = TimeOnly.Parse("20:00:00");
        var request = new GetFreeRoomsRequest(today, from, to);

        var response = await client.GetRoomsAvailableForBooking(request, CancellationToken.None);
        var roomToBook = response.Result!.First();

        var bookingRequest = new BookRoomRequest(
            today,
            Auditory: roomToBook.Id.ToString(),
            BeginTime: from,
            EndTime: to,
            Discipline: "name",
            Group: "Мероприятие",
            LoadType: "Студенческое",
            TeacherPkey: null);

        var bookingResponse = await client.BookRoom(bookingRequest, CancellationToken.None);
        return bookingResponse;
    }

    private IBookingClient SetupClient()
    {
        return new BookingClient(
            new HttpClient(),
            new BookingClientSettings(
                ApiGatewayUrl: "http://sked-tst.dit.urfu.ru:8100/",
                Username: "mediaroom",
                Password: "r3cife0a9h"));
    }
}