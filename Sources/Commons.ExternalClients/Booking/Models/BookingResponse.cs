namespace Commons.ExternalClients.Booking.Models;

public record BookingResponse<TResult>(bool IsOk, TResult? Result, string[]? Errors, bool ShouldRetry);

public record BookingResponse(bool IsOk, string[]? Errors, bool ShouldRetry)
{
    public static BookingResponse<TResult> FromErrors<TResult>(string[] errors, bool shouldRetry)
    {
        return new BookingResponse<TResult>(IsOk: false, Result: default, errors, shouldRetry);
    }
    
    public static BookingResponse FromErrors(string[] errors, bool shouldRetry)
    {
        return new BookingResponse(IsOk: false, errors, shouldRetry);
    }

    public static BookingResponse<TResult> FromResult<TResult>(TResult result)
    {
        return new BookingResponse<TResult>(IsOk: true, result, Errors: null, ShouldRetry: false);
    } 
    
    public static BookingResponse Ok()
    {
        return new BookingResponse(IsOk: true, Errors: null, ShouldRetry: false);
    } 
}
