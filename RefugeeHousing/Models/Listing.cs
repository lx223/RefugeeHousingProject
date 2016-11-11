using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefugeeHousing.Models
{
    public class Listing
    {
        public int Id { get; set; }

        [Required]
        public string LanguagesSpoken { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public int NumberOfBedrooms { get; set; }
        public bool Furnished { get; set; }
        public string Appliances { get; set; }
        public bool Elevator { get; set; } 
    }
}