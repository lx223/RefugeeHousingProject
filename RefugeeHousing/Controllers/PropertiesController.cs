using System.Linq;
using System.Web.Mvc;
using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.Controllers
{
    [Localization]
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
            using (var db = new ApplicationDbContext())
            {
                var requestedListing = db.Listings.Find(id);
                if (requestedListing == null)
                {
                    return new HttpNotFoundResult("Listing " + id + " does not exist");
                }
                return View(requestedListing);
            }
        }
    }
}