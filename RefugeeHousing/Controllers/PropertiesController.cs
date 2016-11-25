using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RefugeeHousing.Models;
using RefugeeHousing.Services;

namespace RefugeeHousing.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                return View(db.Listings.ToList());
            }
        }

        public ActionResult Details(int id)
        {
            var propertyListingService = new PropertyListingService();
            var listing = propertyListingService.GetListing(id);
            if (listing == null)
            {
                return new HttpNotFoundResult("Listing " + id + " does not exist");
            }

            var currentUserId = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                ViewBag.User = UserIdentityService.GetUser(db, currentUserId);
            }
           
            return View(listing);
        }
    }
}