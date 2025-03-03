namespace WeatherAppApi.Models.Responses
{
    public class WeatherForecastResponse
    {
        public string Cod { get; set; }
        public List<ForecastItem> List { get; set; }
    }

    public class ForecastItem
    {
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Wind Wind { get; set; }
        public string dt_txt { get; set; }
    }

}
