using AutoMapper;
using Weather_server.Models.Backend;
using Weather_server.Models.Client;

namespace Weather_server.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<WeatherModel, Weather>()
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DateUpdated, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}