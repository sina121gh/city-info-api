using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models.DTOs;

namespace CityInfo.API.AutoMapperProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityWithoutSightsDto>();
            CreateMap<City, CityDto>();
        }
    }
}
