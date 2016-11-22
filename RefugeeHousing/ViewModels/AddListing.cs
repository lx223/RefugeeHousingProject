namespace RefugeeHousing.ViewModels
{
    public class AddListing
    {
        public string LanguagesSpoken { get; set; }
        public string PlaceId { get; set; }
        public decimal Price { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool Furnished { get; set; }
        public string Appliances { get; set; }
        public bool Elevator { get; set; }
    }
}