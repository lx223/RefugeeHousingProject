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
    }
}