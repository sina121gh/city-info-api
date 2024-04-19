using CityInfo.API.Models.DTOs;
using CityInfo.API.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/sights")]
    public class CitySightsController : ControllerBase
    {

        #region DI

        private readonly ILogger<CitySightsController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;

        public CitySightsController(ILogger<CitySightsController> logger,
            IMailService mailService,
            CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore;

        }

        #endregion

        #region GET

        [HttpGet]
        public ActionResult<IEnumerable<CitySightDto>> GetSights(int cityId)
        {
            try
            {
                CityDto? city = _citiesDataStore
                .Cities.SingleOrDefault
                (c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} not found");
                    return NotFound();
                }

                return Ok(city.Sights);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"an unhandled exception has occurred getting city with id {cityId}", ex);
                return StatusCode(500, "an unhandled exception has occurred");
            }
        }

        [HttpGet("{sightId}")]
        public ActionResult<CitySightDto> GetSight(int cityId, int sightId)
        {
            CityDto? city = _citiesDataStore
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

            CityDto? city = _citiesDataStore.Cities
                .SingleOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            int lastSightId = _citiesDataStore.Cities
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

            CityDto? city = _citiesDataStore.Cities
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

            CityDto? city = _citiesDataStore.Cities
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

        #region DELETE

        [HttpDelete("{sightId}")]
        public ActionResult DeleteSight(int cityId, int sightId)
        {
            #region Find City

            CityDto? city = _citiesDataStore.Cities
                .SingleOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            #endregion

            #region Find Sight

            CitySightDto? sight = city.Sights
                .SingleOrDefault(s => s.Id == sightId);

            if (sight == null)
                return NotFound();

            #endregion

            city.Sights.Remove(sight);
            _mailService.Send("sina121gh@gmail.com", "Delete Alert", $"<h1>Sight {sight.Name} with Id {sightId} Deleted</h1>");

            return NoContent();

        }

        #endregion
    }
}
