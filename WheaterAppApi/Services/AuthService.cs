using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherAppApi.Models.Requests;
using WheaterAppApi.Models;

namespace WeatherAppApi.Services
{
    public class AuthService
    {
        private IConfiguration _configuration;
        private UserService _userService;
        private SignInManager<User> _signInManager;

        public AuthService(IConfiguration configuration, UserService user)
        {
            _configuration = configuration;
            _userService = user;
        }

        public async Task<string> LoginUser(UserLoginRequest userDto)
        {
            var user = await _userService.FindByLogin(userDto.Login);
            if (user == null)
                throw new ApplicationException("User Not Found.");

            var checkPassword = BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password);
            if (!checkPassword)
                throw new ApplicationException("Invalid password.");

            return GeneretateJWT(user);
        }

        private string GeneretateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login!),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
