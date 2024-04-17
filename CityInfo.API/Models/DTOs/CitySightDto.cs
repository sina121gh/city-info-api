﻿namespace CityInfo.API.Models.DTOs
{
    public class CitySightDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
