using System.Linq;
using System.Web.Mvc;
using RefugeeHousing.Models;
using RefugeeHousing.Services;

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

        // TODO REF-42: Make POST
        // TODO REF-42: Route to something sensible like /Properties/<id>/Contact
        public async System.Threading.Tasks.Task<EmptyResult> ContactOwner(int propertyId)
        {
            await propertyContactService.ContactOwner(propertyId);

            return new EmptyResult();
        }
    }
}