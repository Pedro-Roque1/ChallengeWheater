using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using WeatherAppApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace WheaterAppApi.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "b01aacb1f4270253640b682b75b223dd";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetWeatherData<T>(string cityName, string endpoint)
        {
            T result = default;
            var url = $"https://api.openweathermap.org/data/2.5/{endpoint}";
            var parameters = $"?q={cityName}&appid={_apiKey}&units=metric&lang=pt_br";

            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync(parameters).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(jsonString);
            }

            return result;
        }

    }

}
