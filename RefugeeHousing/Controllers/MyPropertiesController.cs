using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Controllers
{
    [Authorize]
    public class MyPropertiesController : Controller
    {
        private readonly IPropertyListingService propertyListingService;
        private readonly IUserIdentityService userIdentityService;

        public MyPropertiesController(IPropertyListingService propertyListingService, IUserIdentityService userIdentityService)
        {
            this.propertyListingService = propertyListingService;
            this.userIdentityService = userIdentityService;
        }

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                ViewBag.User = userIdentityService.GetUser(db, currentUserId);
            }

            return View(propertyListingService.GetListings(currentUserId).ToList());
        }

        // GET: ListingViewModel
        [HttpGet]
        public ActionResult Add()
        {
            var listingViewModel = new ListingViewModel();
            return View(listingViewModel);
        }

        [HttpPost]
        public ActionResult Add(ListingViewModel listingViewModel)
        {
            var currentUserId = User.Identity.GetUserId();
            propertyListingService.AddListingToDatabase(listingViewModel, currentUserId);

            return Redirect("/");
        }


    }
}