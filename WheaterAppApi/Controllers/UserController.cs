using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherAppApi.Models.Requests;
using WeatherAppApi.Services;
using WheaterAppApi.Data;

namespace WeatherAppApi.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, ApiDbContext context, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserCreateRequest user)
        {
            try
            {
                await _userService.Create(user);
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating user. Error: " + ex.Message);
            }
        }
    }
}
