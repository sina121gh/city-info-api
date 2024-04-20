using CityInfo.API.Entities;

namespace CityInfo.API.Repositories.Interfaces
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityByIdAsync(int cityId, bool includeSights);

        Task<IEnumerable<CitySight>> GetCitySightsAsync(int cityId);
        Task<CitySight?> GetCitySightByIdAsync(int cityId, int sightId);
    }
}
