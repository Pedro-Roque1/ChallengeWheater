using WheaterAppApi.Models;

namespace WeatherAppApi.Models.Requests
{
    public class FavoriteCityRequest
    {
        public required string Name { get; set; }
        public int UserId { get; set; }
    }
}
