using CityInfo.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/sights")]
    public class CitySightsController : ControllerBase
    {
        //[HttpGet]
        //public ActionResult GetSights()
        //{
        //    return Ok(CitiesDataStore.Current
        //        .Cities.Select(c => c.Sights));
        //}

        [HttpGet]
        public ActionResult<IEnumerable<CitySightDto>> GetSights(int cityId)
        {
            CityDto city = CitiesDataStore.Current
                .Cities.SingleOrDefault
                (c => c.Id == cityId);

            if (city == null)
                return NotFound();

            return Ok(city.Sights);
        }

        [HttpGet("{sightId}")]
        public ActionResult<CitySightDto> GetSight(int cityId, int sightId)
        {
            CityDto? city = CitiesDataStore.Current
                .Cities.SingleOrDefault
                (c => c.Id == cityId);

            if (city == null)
                return NotFound("city not found");

            CitySightDto? citySight = city.Sights
                .SingleOrDefault(s => s.Id == sightId);

            if (citySight == null)
                return NotFound("sight not found");

            return Ok(citySight);
        }
    }
}
