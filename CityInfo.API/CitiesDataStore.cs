using CityInfo.API.Models.DTOs;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Tehran",
                    Description = "The capital of Iran",
                },

                new CityDto()
                {
                    Id = 2,
                    Name = "Kashan",
                    Description = "My City",
                },

                new CityDto()
                {
                    Id = 3,
                    Name = "Shiraz",
                    Description = "a beautiful city",
                },
            };
        }
    }
}
