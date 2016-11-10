using System.Linq;
using System.Web.Mvc;
using RefugeeHousing.Models;

namespace RefugeeHousing.Controllers
{
    public class ListingsController : Controller
    {
        public ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var listingsList = (new ListingsList {Listings = db.Listings.ToList()});
                return View(listingsList);
            }
        }
    }
}