using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Controllers
{
    [Authorize]
    public class MyPropertiesController : Controller
    {
        // GET: ListingViewModel
        [HttpGet]
        public ActionResult Add()
        {
            var addListing = new ListingViewModel();
            return View(addListing);
        }

        [HttpPost]
        public ActionResult Add(ListingViewModel listingViewModel)
        {
            using (var db = new ApplicationDbContext())
            {
                var locationId = listingViewModel.PlaceId;
                var location = LocationRepository.GetOrCreateLocation(db, locationId);
                
                var currentUserId = User.Identity.GetUserId();
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

            return Redirect("/");
        }
    }
}