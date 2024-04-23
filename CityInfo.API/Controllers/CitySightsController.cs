using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models.DTOs;
using CityInfo.API.Repositories.Interfaces;
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
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitySightsController(ILogger<CitySightsController> logger,
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        #endregion

        #region GET

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitySightDto>>> GetSights(int cityId)
        {
            try
            {
                if (!await _cityInfoRepository.DoesCityExistAsync(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} not found");
                    return NotFound();
                }

                var citySights = await _cityInfoRepository.GetCitySightsAsync(cityId);

                return Ok(_mapper.Map<IEnumerable<CitySightDto>>(citySights));

            }
            catch (Exception)
            {
                _logger.LogCritical($"an unhandled exception has occurred");
                return StatusCode(500, "an unhandled exception has occurred");
            }
        }

        [HttpGet("{sightId}")]
        public async Task<ActionResult<CitySightDto>> GetSight(int cityId, int sightId)
        {
            try
            {
                if (!await _cityInfoRepository.DoesCityExistAsync(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} not found");
                    return NotFound();
                }

                var citySight = await _cityInfoRepository.GetCitySightByIdAsync(cityId, sightId);

                if (citySight == null)
                {
                    _logger.LogInformation($"Sight with city Id {cityId} & id {sightId} not found");
                    return NotFound();
                }

                return Ok(_mapper.Map<CitySightDto>(citySight));

            }
            catch (Exception)
            {
                _logger.LogCritical($"an unhandled exception has occurred");
                return StatusCode(500, "an unhandled exception has occurred");
            }
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<ActionResult<CitySightDto>> CreateSight(
            int cityId,
            /* [FromBody] */ CitySightForCreationDto sight)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _cityInfoRepository.DoesCityExistAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} not found");
                return NotFound();
            }

            CitySight sightToAdd = _mapper.Map<CitySight>(sight);

            _cityInfoRepository.AddCitySight(cityId, sightToAdd);
            await _cityInfoRepository.SaveChangesAsync();

            CitySightDto createdSight = _mapper.Map<CitySightDto>(sightToAdd);

            return CreatedAtAction("GetSight", new
            {
                cityId = cityId,
                sightId = sightToAdd.Id
            }, createdSight);
        }

        #endregion

        #region PUT

        [HttpPut("{sightId}")]
        public async Task<ActionResult> UpdateSight(int cityId, int sightId,
            CitySightForUpdateDto sight)
        {
            // Model Validation
            if (!ModelState.IsValid)
                return BadRequest();


            // Find City
            if (!await _cityInfoRepository.DoesCityExistAsync(cityId))
                return NotFound();

            // Find Sight
            CitySight? sightToUpdate = await _cityInfoRepository
                .GetCitySightByIdAsync(cityId, sightId);

            if (sightToUpdate == null)
                return NotFound();


            // Update Sight
            _mapper.Map(sight, sightToUpdate);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region PATCH

        [HttpPatch("{sightId}")]
        public async Task<ActionResult<CitySightDto>> UpdateSightPartially(int cityId, int sightId,
            JsonPatchDocument<CitySightForUpdateDto> patchDocument)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check City
            if (!await _cityInfoRepository.DoesCityExistAsync(cityId))
                return NotFound();

            // Find Sight
            CitySight? sightEntity = await _cityInfoRepository
                .GetCitySightByIdAsync(cityId, sightId);

            if (sightEntity == null)
                return NotFound();

            CitySightForUpdateDto sightToPatch = _mapper.Map<CitySightForUpdateDto>(sightEntity);

            patchDocument.ApplyTo(sightToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(sightToPatch))
                return BadRequest(ModelState);

            _mapper.Map(sightToPatch, sightEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();

        }

        #endregion

        #region DELETE

        [HttpDelete("{sightId}")]
        public async Task<ActionResult> DeleteSight(int cityId, int sightId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check City
            if (!await _cityInfoRepository.DoesCityExistAsync(cityId))
                return NotFound();

            // Find Sight
            CitySight? sight = await _cityInfoRepository
                .GetCitySightByIdAsync(cityId, sightId);

            if (sight == null)
                return NotFound();

            _cityInfoRepository.DeleteSight(sight);
            await _cityInfoRepository.SaveChangesAsync();

            // Send Email
            await _mailService.Send("sina121gh@gmail.com", "Delete Alert", $"<h1>Sight {sight.Name} with Id {sightId} Deleted</h1>");

            return NoContent();

        }

        #endregion
    }
}
