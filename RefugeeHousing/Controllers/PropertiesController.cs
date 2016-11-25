using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        private readonly IPropertyContactService propertyContactService;
        private readonly IPropertyListingService propertyListingService;
        private readonly IUserIdentityService userIdentityService;

        public PropertiesController(IPropertyContactService propertyContactService, IPropertyListingService propertyListingService, IUserIdentityService userIdentityService)
        {
            this.propertyContactService = propertyContactService;
            this.propertyListingService = propertyListingService;
            this.userIdentityService = userIdentityService;
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
            var listing = propertyListingService.GetListing(id);
            if (listing == null)
            {
                return new HttpNotFoundResult("Listing " + id + " does not exist");
            }

            var currentUserId = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                ViewBag.User = userIdentityService.GetUser(db, currentUserId);
            }
           
            return View(listing);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> ContactOwner(PropertyEnquiry propertyEnquiry)
        {
            await propertyContactService.ContactOwner(propertyEnquiry);

            return RedirectToAction("Details", new {id = propertyEnquiry.PropertyId});
        }
    }
}