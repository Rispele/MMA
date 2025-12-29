namespace Commons.ExternalClients.Booking;

public record BookingClientSettings(
    string ApiGatewayUrl,
    string Username,
    string Password);