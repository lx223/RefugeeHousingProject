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
        // GET: ListingViewModel
        [HttpGet]
        public ActionResult Add()
        {
            var addListing = new ListingViewModel();
            return View(addListing);
        }

        [HttpPost]
        public ActionResult Add(ListingViewModel listingViewModel)
        {
            var currentUserId = User.Identity.GetUserId();
            PropertyListingService.AddListingToDatabase(listingViewModel, currentUserId);

            return Redirect("/");
        }


    }
}