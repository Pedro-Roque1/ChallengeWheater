namespace WeatherAppApi.Models.Dtos
{
    public class WeatherResponseDto
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public string WeatherDescription { get; set; }
        public string Icon { get; set; }
        public string Country { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string DateTxt { get; set; }

    }
}
