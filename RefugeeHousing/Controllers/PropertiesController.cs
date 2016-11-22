using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        private readonly IPropertyContactService propertyContactService;

        public PropertiesController(IPropertyContactService propertyContactService)
        {
            this.propertyContactService = propertyContactService;
        }

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

        [HttpPost]
        public async Task<EmptyResult> ContactOwner(PropertyEnquiry propertyEnquiry)
        {
            await propertyContactService.ContactOwner(propertyEnquiry);

            return new EmptyResult();
        }
    }
}