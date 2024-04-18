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
                    Sights = new List<CitySightDto>()
                    {
                        new CitySightDto() 
                        {
                            Id = 1,
                            Name = "Jaye Didanie 1",
                            Description = "This is Jaye Didanie 1",
                        },

                        new CitySightDto()
                        {
                            Id = 2,
                            Name = "Jaye Didanie 2",
                            Description = "This is Jaye Didanie 2",
                        },
                    },
                },

                new CityDto()
                {
                    Id = 2,
                    Name = "Kashan",
                    Description = "My City",
                    Sights = new List<CitySightDto>()
                    {
                        new CitySightDto()
                        {
                            Id = 3,
                            Name = "Jaye Didanie 3",
                            Description = "This is Jaye Didanie 3",
                        },

                        new CitySightDto()
                        {
                            Id = 4,
                            Name = "Jaye Didanie 4",
                            Description = "This is Jaye Didanie 4",
                        },
                    },
                },

                new CityDto()
                {
                    Id = 3,
                    Name = "Shiraz",
                    Description = "a beautiful city",
                    Sights = new List<CitySightDto>()
                    {
                        new CitySightDto()
                        {
                            Id = 5,
                            Name = "Jaye Didanie 5",
                            Description = "This is Jaye Didanie 5",
                        },

                        new CitySightDto()
                        {
                            Id = 6,
                            Name = "Jaye Didanie 6",
                            Description = "This is Jaye Didanie 6",
                        },
                    },
                },
            };
        }
    }
}
