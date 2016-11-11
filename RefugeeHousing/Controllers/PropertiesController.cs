using System.Linq;
using System.Web.Mvc;
using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.Controllers
{
    [Localization]
    public class PropertiesController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var db = new ApplicationDbContext())
            {
                return View(db.Listings.ToList());
            }
        }

        public ActionResult Details(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var db = new ApplicationDbContext())
            {
                var requestedListing = db.Listings.Find(id);
                if (requestedListing == null)
                {
                    return new HttpNotFoundResult("Listing does not exist");
                }
                return View(requestedListing);
            }
        }
    }
}