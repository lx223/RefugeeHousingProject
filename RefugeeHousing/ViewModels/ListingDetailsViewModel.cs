namespace RefugeeHousing.ViewModels
{
    public class ListingDetailsViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool Furnished { get; set; }
        public string Appliances { get; set; }
        public bool Elevator { get; set; }
    }
}