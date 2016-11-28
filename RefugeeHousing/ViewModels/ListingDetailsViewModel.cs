using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.ViewModels
{
    public class ListingDetailsViewModel
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public decimal Price { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool Furnished { get; set; }
        public string Appliances { get; set; }
        public bool Elevator { get; set; }
        public string LanguagesSpoken { get; set; }
        public string OwnerId { get; set; }

        public string GetLocation(Language language)
        {
            return (language == Language.Greek) ? Location.GreekName : Location.EnglishName;
        }
    }
}