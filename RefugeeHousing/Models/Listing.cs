using System.ComponentModel.DataAnnotations;

namespace RefugeeHousing.Models
{
    public class Listing
    { 
        public int Id { get; set; }
        [Required]
        public string LanguagesSpoken { get; set; }
        public string LocationId { get; set; }
        [Required]
        public Location Location { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int NumberOfBedrooms { get; set; }
        public bool Furnished { get; set; }
        public string Appliances { get; set; }
        public bool Elevator { get; set; } 
        public string OwnerId { get; set; }
        [Required]
        public ApplicationUser Owner { get; set; }
    }
}