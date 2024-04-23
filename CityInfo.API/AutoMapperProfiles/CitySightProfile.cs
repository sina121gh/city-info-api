using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models.DTOs;

namespace CityInfo.API.AutoMapperProfiles
{
    public class CitySightProfile : Profile
    {
        public CitySightProfile()
        {
            CreateMap<CitySight, CitySightDto>();
            CreateMap<CitySight, CitySightForUpdateDto>();

            CreateMap<CitySightForCreationDto, CitySight>();
            CreateMap<CitySightForUpdateDto, CitySight>();
        }
    }
}
