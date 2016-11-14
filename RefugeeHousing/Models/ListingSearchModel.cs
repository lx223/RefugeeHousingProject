using System.Collections.Generic;

namespace RefugeeHousing.Models
{
    public class ListingSearchModel
    {
        public int? MinBedrooms { get; set; }
        public decimal? MaxPricePerMonth { get; set; }
        public bool? IsFurnished { get; set; }

        public List<Listing> ListingsToDisplay { get; set; }
    }
}