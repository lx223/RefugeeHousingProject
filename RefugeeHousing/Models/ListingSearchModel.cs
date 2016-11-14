using System.Collections.Generic;

namespace RefugeeHousing.Models
{
    public class ListingSearchModel
    {
        public int? MinRooms { get; set; }
        public decimal? MaxRent { get; set; }
        public bool? Furnished { get; set; }

        public List<Listing> ListingsToDisplay { get; set; }
    }
}