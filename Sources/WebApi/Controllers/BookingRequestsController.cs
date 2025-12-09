using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.ModelBinders.GetRequestWithJsonFilter;
using WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;
using WebApi.Models.BookingRequest;
using WebApi.Models.Requests.BookingRequests;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("webapi/booking-requests")]
public class BookingRequestsController(IBookingRequestService bookingRequestService) : ControllerBase
{
    /// <summary>
    /// Получить записи о заявках на бронирование аудиторий
    /// </summary>
    /// <param name="model">Модель поиска страницы с записями</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список записей о заявках на бронирование аудиторий</returns>
    [HttpGet]
    public async Task<ActionResult<BookingRequestsResponseModel>> GetBookingRequests(
        [GetRequestWithJsonFilterModelBinder<GetBookingRequestsModel, BookingRequestsFilterModel, GetBookingRequestSpecification>]
        GetBookingRequestsModel model,
        CancellationToken cancellationToken)
    {
        var result = await bookingRequestService.GetBookingRequestsAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить заявку на бронирование аудиторий
    /// </summary>
    /// <param name="bookingRequestId">Идентификатор заявки</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Заявка на бронирование аудиторий</returns>
    [HttpGet("{bookingRequestId:int}")]
    public async Task<ActionResult<BookingRequestModel>> GetBookingRequestById(
        int bookingRequestId,
        CancellationToken cancellationToken)
    {
        var bookingRequest = await bookingRequestService.GetBookingRequestByIdAsync(bookingRequestId, cancellationToken);
        return Ok(bookingRequest);
    }

    /// <summary>
    /// Выполнить автокомплит имени ведущего мероприятия
    /// </summary>
    /// <param name="name">Подпоследовательность полного имени ведущего</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список пользователей, чье полное имя дополняет входную строку</returns>
    [HttpGet("autocomplete")]
    public async Task<ActionResult<IEnumerable<AutocompleteEventHostResponseModel>>> AutocompleteEventHostName(
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var autocompleteNames = await bookingRequestService.AutocompleteEventHostNameAsync(name, cancellationToken);
        return Ok(autocompleteNames);
    }

    /// <summary>
    /// Создать заявку на бронирование аудиторий
    /// </summary>
    /// <param name="model">Модель создания заявки</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Созданная заявка</returns>
    [HttpPost]
    public async Task<IActionResult> CreateBookingRequest(
        [FromBody] CreateBookingRequestModel model,
        CancellationToken cancellationToken)
    {
        var created = await bookingRequestService.CreateBookingRequestAsync(model, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// Изменить заявку на бронирование аудитории
    /// </summary>
    /// <param name="bookingRequestId">Идентификатор изменяемой заявки</param>
    /// <param name="patch">Модель изменений заявки</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Измененная заявка</returns>
    /// <exception cref="BadHttpRequestException"></exception>
    [HttpPatch("{bookingRequestId:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchBookingRequest(
        int bookingRequestId,
        [FromBody] JsonPatchDocument<PatchBookingRequestModel> patch,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            throw new BadHttpRequestException(string.Join(separator: "; ", errorMessage));
        }

        var patchModel = await bookingRequestService.GetBookingRequestPatchModel(bookingRequestId, cancellationToken);

        patch.ApplyTo(patchModel);

        if (!TryValidateModel(patchModel))
        {
            return ValidationProblem(ModelState);
        }

        var updated = await bookingRequestService.PatchBookingRequestAsync(bookingRequestId, patchModel, cancellationToken);

        return Ok(updated);
    }
}