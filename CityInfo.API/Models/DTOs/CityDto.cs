namespace CityInfo.API.Models.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int SightsCount
        {
            get { return Sights.Count; }
        }

        public ICollection<CitySightDto> Sights { get; set; }
        = new List<CitySightDto>();
    }
}
