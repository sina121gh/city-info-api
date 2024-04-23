namespace CityInfo.API.Models.DTOs
{
    public class CityWithoutSightsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
