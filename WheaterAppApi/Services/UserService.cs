using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WeatherAppApi.Models.Requests;
using WheaterAppApi.Data;
using WheaterAppApi.Models;

namespace WeatherAppApi.Services
{
    public class UserService
    {
        private ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Create(UserCreateRequest userRequest)
        {
            if (FindByLogin(userRequest.Login).Result != null)
                throw new Exception("Already registered username.");

            var user = new User
            { 
                Login = userRequest.Login,
                Email = userRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password)
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> FindByLogin(string login)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Login.ToUpper() == login.ToUpper());
        }
    }
}
