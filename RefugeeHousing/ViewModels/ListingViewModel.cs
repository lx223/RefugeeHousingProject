using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.ViewModels
{
    public class ListingViewModel
    {
        public string LanguagesSpoken { get; set; }
        public string PlaceId { get; set; }
        public Location Location { get; set; }
        public int Price { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool Furnished { get; set; }
        public string Appliances { get; set; }
        public bool Elevator { get; set; }

        public string GetLocation(Language language)
        {
            return (language == Language.Greek) ? Location.GreekName : Location.EnglishName;
        }
    }
}