using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RefugeeHousing.Services;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Controllers
{
    [Authorize]
    public class MyPropertiesController : Controller
    {
        private readonly IPropertyListingService propertyListingService;

        public MyPropertiesController(IPropertyListingService propertyListingService)
        {
            this.propertyListingService = propertyListingService;
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