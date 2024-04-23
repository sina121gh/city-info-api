using CityInfo.API.Entities;

namespace CityInfo.API.Repositories.Interfaces
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityByIdAsync(int cityId, bool includeSights);
        Task<bool> DoesCityExistAsync(int cityId);

        Task<IEnumerable<CitySight>> GetCitySightsAsync(int cityId);
        Task<CitySight?> GetCitySightByIdAsync(int cityId, int sightId);
        Task AddCitySight(int cityId, CitySight sight);
        void DeleteSight(CitySight sight);

        Task<bool> SaveChangesAsync();
    }
}
