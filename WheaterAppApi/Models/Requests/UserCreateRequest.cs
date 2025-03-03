namespace WeatherAppApi.Models.Requests
{
    public class UserCreateRequest
    {
        public required string Login { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }
}