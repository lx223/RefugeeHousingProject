using System;

namespace RefugeeHousing.Models
{
    public class Listing
    {
        public int Id { get; set; }
        
        public string LanguagesSpoken { get; set; }
        public Decimal Price { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool Furnished { get; set; }
        public string Appliances { get; set; }
        public bool Elevator { get; set; } 
    }
}