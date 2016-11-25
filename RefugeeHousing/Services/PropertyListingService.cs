using RefugeeHousing.ApiAccess;
using RefugeeHousing.Models;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Services
{
    public interface IPropertyListingService
    {
        void AddListingToDatabase(ListingViewModel listingViewModel, string currentUserId);
    }

    public class PropertyListingService : IPropertyListingService
    {
        private readonly ILocationRepository locationRepository;

        public PropertyListingService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public void AddListingToDatabase(ListingViewModel listingViewModel, string currentUserId)
        {
            using (var db = new ApplicationDbContext())
            {
                var locationId = listingViewModel.PlaceId;
                var location = locationRepository.GetOrCreateLocation(db, locationId);
                var currentUser = UserIdentityService.GetUser(db, currentUserId);

                var listing = new Listing
                {
                    Appliances = listingViewModel.Appliances,
                    Elevator = listingViewModel.Elevator,
                    Furnished = listingViewModel.Furnished,
                    Price = listingViewModel.Price,
                    LanguagesSpoken = listingViewModel.LanguagesSpoken,
                    Location = location,
                    LocationId = locationId,
                    NumberOfBedrooms = listingViewModel.NumberOfBedrooms,
                    Owner = currentUser,
                    OwnerId = currentUserId
                };

                db.Listings.Add(listing);
                db.SaveChanges();
            }
        }
    }
}