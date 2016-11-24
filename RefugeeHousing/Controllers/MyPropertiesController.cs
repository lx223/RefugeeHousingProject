using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RefugeeHousing.ApiAccess;
using RefugeeHousing.Models;
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
                Location location;
                if (db.Locations.Any(l => l.Id == locationId))
                {
                    location = db.Locations.Single(l => l.Id == locationId);
                }
                else
                {
                    var placeLookUpService = new PlaceLookUpService();
                    var englishName = placeLookUpService.FindLocalityNameByLocationId(locationId, PlaceLookUpService.Languages.English);
                    var greekName = placeLookUpService.FindLocalityNameByLocationId(locationId, PlaceLookUpService.Languages.Greek);
                    location = new Location {EnglishName = englishName, GreekName = greekName, Id = locationId};
                    db.Locations.Add(location);
                }

                var currentUserId = User.Identity.GetUserId();
                var currentUser = db.Users.Single(u => u.Id == currentUserId);

                var listing = new Listing
                {
                    Appliances = addListing.Appliances, Elevator = addListing.Elevator,
                    Furnished = addListing.Furnished, Price = addListing.Price,
                    LanguagesSpoken = addListing.LanguagesSpoken, Location = location,
                    LocationId = locationId, NumberOfBedrooms = addListing.NumberOfBedrooms,
                    Owner = currentUser, OwnerId = currentUserId
                };

                db.Listings.Add(listing);
                db.SaveChanges();
            }

            return Redirect("/");
        }
    }
}