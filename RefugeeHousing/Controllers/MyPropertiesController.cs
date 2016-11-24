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
        // GET: AddListing
        [HttpGet]
        public ActionResult Add()
        {
            var addListing = new AddListing();
            return View(addListing);
        }

        [HttpPost]
        public ActionResult Add(AddListing addListing)
        {
            using (var db = new ApplicationDbContext())
            {
                var locationId = addListing.PlaceId;
                var location = LocationRepository.GetOrCreateLocation(db, locationId);
                
                var currentUserId = User.Identity.GetUserId();
                var currentUser = UserIdentityService.GetUser(db, currentUserId);

                var listing = new Listing
                {
                    Appliances = addListing.Appliances,
                    Elevator = addListing.Elevator,
                    Furnished = addListing.Furnished,
                    Price = addListing.Price,
                    LanguagesSpoken = addListing.LanguagesSpoken,
                    Location = location,
                    LocationId = locationId,
                    NumberOfBedrooms = addListing.NumberOfBedrooms,
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