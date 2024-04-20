using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class CitySight
    {
        public CitySight(string name)
        {
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int CityId { get; set; }

        #region Relations

        [ForeignKey("CityId")]
        public City? City { get; set; }

        #endregion

    }
}
