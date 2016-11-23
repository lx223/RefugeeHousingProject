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
                var englishName = FindLocalityNameByPlaceId(addListing.PlaceId, false);
                var greekName = FindLocalityNameByPlaceId(addListing.PlaceId, true);



//                if (!db.Locations.Any(l => l.Id == addListing.PlaceId))
//                {
//                    HttpWebRequest httpWebRequest = HttpWebRequest.Create(url);
//                    httpWebRequest.Method = "GET";
//
//                    db.Locations.Add()
//                }
//                var currentUserId = User.Identity.GetUserId();
//                var currentUser = db.Users.Single(u => u.Id == currentUserId);
//
//                listing.Owner = currentUser;
//                db.Listings.Add(listing);
//
//                db.SaveChanges();
            }

            return Redirect("/");
        }

        private static string FindLocalityNameByPlaceId(string placeId, bool resultInGreek)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/place/details/json?");
            var request = new RestRequest(Method.GET);
            request.AddParameter("key", "AIzaSyAi0qtOthsQrOMs_IrfghpmlBKqaSUHmI0");
            request.AddParameter("placeid", placeId);

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