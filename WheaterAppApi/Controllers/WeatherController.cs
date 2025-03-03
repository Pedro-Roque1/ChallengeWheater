using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAppApi.Models.Dtos;
using WheaterAppApi.Data;
using WheaterAppApi.Services;
using AutoMapper;
using WeatherAppApi.Models.Responses;

namespace WheatherAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IMapper _mapper;
        private readonly WeatherService _weatherService;
        public WeatherController(ILogger<WeatherController> logger, ApiDbContext context, WeatherService ws, IMapper mapper)
        {
            _logger = logger;
            _weatherService = ws;
            _mapper = mapper;
        }

        [HttpGet("current/{city}")]
        public async Task<IActionResult> GetCurrentWeather(string city)
        {
            try
            {
                var forecast = await _weatherService.GetWeatherData<WeatherResponse>(city, "weather");
                if (forecast == null)
                    return NotFound("City not found or no forecast available");

                var weatherDto = _mapper.Map<WeatherResponseDto>(forecast);

                return Ok(weatherDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar os dados do clima: " + ex.Message);
            }
        }

        [HttpGet("forecast/{city}/{days}")]
        public async Task<IActionResult> GetForecast(string city, int days)
        {
            try
            {
                var forecast = await _weatherService.GetWeatherData<WeatherForecastResponse>(city, "forecast");
                if (forecast == null || forecast.List.Count() <= 0)
                    return NotFound("City not found or no forecast available");

                var forecasts = forecast.List
                    .GroupBy(f => f.dt_txt.Split(' ')[0])
                    .Take(days)
                    .Select(f => _mapper.Map<WeatherResponseDto>(f.First()))
                    .ToList();

                return Ok(forecasts);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar os dados do clima: " + ex.Message);
            }
        }
    }
}
