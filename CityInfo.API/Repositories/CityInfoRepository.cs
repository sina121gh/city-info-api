using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using CityInfo.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repositories
{
    public class CityInfoRepository : ICityInfoRepository
    {

        #region DI

        private readonly CityInfoDbContext _context;

        public CityInfoRepository(CityInfoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<City?> GetCityByIdAsync(int cityId, bool includeSights)
        {
            if (includeSights)
            {
                return await _context.Cities
                    .Include(c => c.Sights)
                    .SingleOrDefaultAsync(c => c.Id == cityId);
            }

            return await _context.Cities.FindAsync(cityId);
        }

        public async Task<CitySight?> GetCitySightByIdAsync(int cityId, int sightId)
        {
            return await _context.CitySights
                .SingleOrDefaultAsync(s => s.CityId == cityId && s.Id == sightId);
        }

        public async Task<IEnumerable<CitySight>> GetCitySightsAsync(int cityId)
        {
            return await _context.CitySights
                .Where(s => s.CityId == cityId)
                .ToListAsync();
        }
    }
}
