using CityInfo.API.Models.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/sights")]
    public class CitySightsController : ControllerBase
    {
        #region GET

        [HttpGet]
        public ActionResult<IEnumerable<CitySightDto>> GetSights(int cityId)
        {
            CityDto? city = CitiesDataStore.Current
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

        #endregion

        #region POST

        [HttpPost]
        public ActionResult<CitySightDto> CreateSight(
            int cityId,
            /* [FromBody] */ CitySightForCreationDto sight)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            CityDto? city = CitiesDataStore.Current.Cities
                .SingleOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            int lastSightId = CitiesDataStore.Current.Cities
                .SelectMany(c => c.Sights)
                .Max(s => s.Id);

            CitySightDto newSight = new CitySightDto()
            {
                Id = ++lastSightId,
                Name = sight.Name,
                Description = sight.Description,
            };

            city.Sights.Add(newSight);

            return CreatedAtAction("GetSight",
                new
                {
                    cityId = cityId,
                    sightId = newSight.Id,
                },
                newSight
                );
        }

        #endregion

        #region PUT

        [HttpPut("{sightId}")]
        public ActionResult<CitySightDto> UpdateSight(int cityId, int sightId, CitySightForUpdateDto sight)
        {
            #region Model Validation

            if (!ModelState.IsValid)
                return BadRequest();

            #endregion

            #region Find City

            CityDto? city = CitiesDataStore.Current.Cities
                .SingleOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            #endregion

            #region Find Sight

            CitySightDto? currentSight = city.Sights
                .SingleOrDefault(s => s.Id == sightId);

            if (currentSight == null)
                return NotFound();

            #endregion

            #region Update Sight

            currentSight.Name = sight.Name;
            currentSight.Description = sight.Description;

            #endregion

            return NoContent();
        }

        #endregion

        #region PATCH

        [HttpPatch("{sightId}")]
        public ActionResult<CitySightDto> UpdateSightPartially(int cityId, int sightId,
            JsonPatchDocument<CitySightForUpdateDto> patchDocument)
        {
            #region Find City

            CityDto? city = CitiesDataStore.Current.Cities
                .SingleOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            #endregion

            #region Find Sight

            CitySightDto? currentSight = city.Sights
                .SingleOrDefault(s => s.Id == sightId);

            if (currentSight == null)
                return NotFound();

            #endregion

            CitySightForUpdateDto sightToPatch = new CitySightForUpdateDto()
            {
                Name = currentSight.Name,
                Description = currentSight.Description,
            };

            patchDocument.ApplyTo(sightToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(sightToPatch))
                return BadRequest(ModelState);

            currentSight.Name = sightToPatch.Name;
            currentSight.Description = sightToPatch.Description;

            return NoContent();
        }

        #endregion
    }
}
