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
            var currentUserId = User.Identity.GetUserId();
            var listing = PropertyListingService.GetListing(id, currentUserId);
            if (listing == null)
            {
                return new HttpNotFoundResult("Listing " + id + " does not exist");
            }
            return View(listing);
        }
    }
}