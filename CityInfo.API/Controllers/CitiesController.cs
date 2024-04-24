using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models.DTOs;
using CityInfo.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        #region DI

        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutSightsDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();

            return Ok(_mapper.Map<IEnumerable<CityWithoutSightsDto>>(cities));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includeSights = false)
        {
            City? city = await _cityInfoRepository.GetCityByIdAsync(id, includeSights);

            if (city == null)
                return NotFound();

            if (includeSights)
                return Ok(_mapper.Map<CityDto>(city));
            else
                return Ok(_mapper.Map<CityWithoutSightsDto>(city));
        }
    }
}
