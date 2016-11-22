using System.ComponentModel.DataAnnotations;

namespace RefugeeHousing.Models
{
    public class Location
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string EnglishName { get; set; }
        [Required]
        public string GreekName { get; set; }
    }
}