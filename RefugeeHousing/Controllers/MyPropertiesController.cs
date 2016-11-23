using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RefugeeHousing.ApiClient;
using RefugeeHousing.Models;
using RefugeeHousing.ViewModels;
using RestSharp;

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
                    var englishName = FindLocalityNameByLocationId(locationId, false);
                    var greekName = FindLocalityNameByLocationId(locationId, true);
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

        private static string FindLocalityNameByLocationId(string locationId, bool resultInGreek)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/place/details/json?");
            var request = new RestRequest(Method.GET);
            request.AddParameter("key", "AIzaSyAi0qtOthsQrOMs_IrfghpmlBKqaSUHmI0");
            request.AddParameter("placeid", locationId);

            if (resultInGreek)
            {
                request.AddParameter("language", "el");
            }

            var response = client.Execute<PlaceIdLookUpResult>(request);
            var addressComponents = response.Data.Result.AddressComponents;
            var localityComponent = addressComponents.Single(s => s.Types.Contains("locality"));
            return localityComponent.LongName;
        }
    }
}