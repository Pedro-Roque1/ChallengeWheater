using AutoMapper;
using WeatherAppApi.Models.Dtos;
using WeatherAppApi.Models.Responses;

namespace WeatherAppApi.Models.Profiles
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<WeatherResponse, WeatherResponseDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.main.temp))
            .ForMember(dest => dest.MaxTemp, opt => opt.MapFrom(src => src.main.temp_max))
            .ForMember(dest => dest.MinTemp, opt => opt.MapFrom(src => src.main.temp_min))
            .ForMember(dest => dest.WeatherDescription, opt => opt.MapFrom(src => src.weather.FirstOrDefault().description))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.weather.FirstOrDefault().icon))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.sys.country));

            CreateMap<ForecastItem, WeatherResponseDto>()
            .ForMember(dest => dest.DateTxt, opt => opt.MapFrom(src => src.dt_txt))
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Main.temp))
            .ForMember(dest => dest.MaxTemp, opt => opt.MapFrom(src => src.Main.temp_max))
            .ForMember(dest => dest.MinTemp, opt => opt.MapFrom(src => src.Main.temp_min))
            .ForMember(dest => dest.WeatherDescription, opt => opt.MapFrom(src => src.Weather.FirstOrDefault().description))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Weather.FirstOrDefault().icon));
        }
    }
}
