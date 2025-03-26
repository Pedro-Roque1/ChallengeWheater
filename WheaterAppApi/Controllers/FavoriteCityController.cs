using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherAppApi.Models.Requests;
using WeatherAppApi.Services;
using WheaterAppApi.Data;
using WheaterAppApi.Models;

namespace WeatherAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteCityController : ControllerBase
    {
        private readonly ILogger<FavoriteCityController> _logger;
        private readonly FavoriteCityService _fCityService;

        public FavoriteCityController(ILogger<FavoriteCityController> logger, ApiDbContext context, FavoriteCityService userService)
        {
            _logger = logger;
            _fCityService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(FavoriteCityRequest fCity)
        {
            try
            {
                await _fCityService.Create(fCity);
                return Ok("City added to favorite cities list.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error adding favorite city. Error: " + ex.Message);
            }
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cities = await _fCityService.GetAll();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return BadRequest("Error when listing favorite city. Error:" + ex.Message);
            }
        }

        [HttpDelete("{cityId}")]
        public async Task<IActionResult> Delete(int cityId)
        {
            try
            {
                await _fCityService.Delete(cityId);
                return Ok("Favorite city successfully removed.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error when deleting favorite city. Error:" + ex.Message);
            }
        }
    }
}
