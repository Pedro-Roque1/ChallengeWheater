using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeatherAppApi.Models.Requests;
using WheaterAppApi.Data;
using WheaterAppApi.Models;

namespace WeatherAppApi.Services
{
    public class FavoriteCityService
    {
        private ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public FavoriteCityService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Create(FavoriteCityRequest fCity)
        {
            var user = await _dbContext.Users.FindAsync(fCity.UserId);
            if (user == null)
                throw new Exception("User not found.");

            var city = await _dbContext.FavoriteCities
                .Where(c => c.Name.ToUpper() == fCity.Name.ToUpper() && c.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (city != null)
                throw new Exception("City ​​already favorited by this user.");

            var favorite = new FavoriteCity
            {
                Name = fCity.Name,
                UserId = fCity.UserId
            };

            user.FavoriteCities?.Add(favorite);
            _dbContext.FavoriteCities.Add(favorite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int fCityId)
        {
            var fCity = await _dbContext.FavoriteCities.FindAsync(fCityId);
            if (fCity == null)
                throw new Exception("City not found");

            _dbContext.FavoriteCities.Remove(fCity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<FavoriteCity>> GetAll()
        {
            return _mapper.Map<List<FavoriteCity>>(await _dbContext.FavoriteCities.ToListAsync());
        }
    }
}
