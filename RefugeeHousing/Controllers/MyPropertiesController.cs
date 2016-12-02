using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NLog;
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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var listing = propertyListingService.GetListing(id);

            if (listing.OwnerId != currentUserId)
            {
                Response.StatusCode = (int) HttpStatusCode.Forbidden;
                Logger.Warn($"The user tried to delete a property which is not owned by the user. " +
                            $"This should not happen often. ActionResult: Delete[HttpPost]. UserId: {currentUserId}." +
                            $"ListingId: {listing.Id}.");
                return new EmptyResult();
            }

            propertyListingService.DeleteListing(id);
            return RedirectToAction("Index");
        }
    }
}