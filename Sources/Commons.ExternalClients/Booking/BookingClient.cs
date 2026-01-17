using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Web;
using Commons.ExternalClients.Booking.Models;

namespace Commons.ExternalClients.Booking;

file record BookingErrorResponse(string[] Errors);

public class BookingClient : IBookingClient
{
    private readonly HttpClient httpClient;

    public BookingClient(HttpClient httpClient, BookingClientSettings settings)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri(settings.ApiGatewayUrl);

        var authHeaderBytes = Encoding.UTF8.GetBytes($"{settings.Username}:{settings.Password}");
        var authHeaderValue = Convert.ToBase64String(authHeaderBytes);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
    }

    public async Task<BookingResponse<RoomInfo[]>> GetAllRooms(CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync("sked/api/auditory/all", cancellationToken);

        return await ProcessResponse<RoomInfo[]>(response, cancellationToken);
    }

    public async Task<BookingResponse<FreeRoomInfo[]>> GetRoomsAvailableForBooking(
        GetFreeRoomsRequest request,
        CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder
        {
            Path = "sked/api/auditory"
        };

        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["date"] = request.Date.ToString("dd.MM.yyyy");
        query["beginTime"] = request.BeginTime.ToString("HH:mm");
        query["endTime"] = request.EndTime.ToString("HH:mm");
        uriBuilder.Query = query.ToString();

        var response = await httpClient.GetAsync(
            uriBuilder.Uri.PathAndQuery,
            cancellationToken);

        return await ProcessResponse<FreeRoomInfo[]>(response, cancellationToken);
    }

    public async Task<BookingResponse<long>> BookRoom(BookRoomRequest request, CancellationToken cancellationToken)
    {
        var innerRequest = new BookRoomInnerRequest(
            request.Date.ToString(),
            request.Auditory,
            request.BeginTime.ToString(),
            request.EndTime.ToString(),
            request.Discipline,
            request.Group,
            request.LoadType,
            request.TeacherPkey);

        var response = await httpClient.PostAsJsonAsync(
            requestUri: "sked/api/order",
            innerRequest,
            cancellationToken);

        return await ProcessResponse<long>(response, cancellationToken);
    }

    public async Task<BookingResponse> DeclineBooking(long eventId, CancellationToken cancellationToken)
    {
        var response = await httpClient.DeleteAsync(
            requestUri: $"sked/api/order/decline?eventId={eventId}",
            cancellationToken);

        return await ProcessResponse(response, cancellationToken);
    }

    public async Task<BookingResponse> ConfirmBooking(long eventId, CancellationToken cancellationToken)
    {
        var response = await httpClient.PatchAsync(
            requestUri: $"sked/api/order/confirm?eventId={eventId}",
            null,
            cancellationToken);

        return await ProcessResponse(response, cancellationToken);
    }

    private static async Task<BookingResponse> ProcessResponse(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return BookingResponse.Ok();
        }

        var (shouldRetry, deduplicated, errors) = await ResolveErrorData(response, cancellationToken);
        return BookingResponse.FromErrors(errors, shouldRetry, deduplicated);
    }

    private static async Task<BookingResponse<TResult>> ProcessResponse<TResult>(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (!response.IsSuccessStatusCode)
        {
            var (shouldRetry, deduplicated, errors) = await ResolveErrorData(response, cancellationToken);
            return BookingResponse.FromErrors<TResult>(errors, shouldRetry, deduplicated);
        }

        var result = await response.Content.ReadFromJsonAsync<TResult>(cancellationToken)
                     ?? throw new InvalidOperationException("Could not deserialize the response");
        return BookingResponse.FromResult(result);
    }

    private static async Task<(bool shouldRetry, bool deduplicated, string[] errors)> ResolveErrorData(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var statusCode = response.StatusCode;
        var statusCodeGroup = (int)statusCode / 100;
        var shouldRetry = statusCodeGroup == 5 || statusCode is HttpStatusCode.TooManyRequests;
        var deduplicated = statusCode is HttpStatusCode.UnprocessableEntity;
        var errors = statusCodeGroup == 5
            ? []
            : (await response.Content.ReadFromJsonAsync<BookingErrorResponse>(cancellationToken: cancellationToken))
            ?.Errors ?? [];
        return (shouldRetry, deduplicated, errors);
    }
}