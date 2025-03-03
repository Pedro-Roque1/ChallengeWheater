using Microsoft.AspNetCore.Mvc;
using WeatherAppApi.Models.Requests;
using WeatherAppApi.Services;
using WheaterAppApi.Data;
using WheaterAppApi.Models;

namespace WeatherAppApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ApiDbContext _context;
        private readonly AuthService _authService;

        public AuthController(ILogger<AuthController> logger, ApiDbContext context, AuthService authService)
        {
            _logger = logger;
            _context = context;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(UserLoginRequest loginRequest)
        {
            string token = await _authService.LoginUser(loginRequest);
            Response.Headers.Add("Authorization", "Bearer " + token);

            return Ok(token);
        }
    }
}
