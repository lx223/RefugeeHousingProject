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
        
        public ActionResult Index(ListingSearchModel listingSearchModel)
        {
            
            using (var db = new ApplicationDbContext())
            {
                var listings = db.Listings.Select(x => x);
                if (listingSearchModel != null)
                {
                    if (listingSearchModel.MinRooms != null)
                    {
                        listings = listings.Where(x => x.NumberOfBedrooms >= (int) listingSearchModel.MinRooms);
                    }
                    if (listingSearchModel.MaxRent != null)
                    {
                        listings = listings.Where(x => x.Price <= (int)listingSearchModel.MaxRent);
                    }
                    if (listingSearchModel.Furnished != null)
                    {
                        listings = listings.Where(x => x.Furnished == (bool) listingSearchModel.Furnished);
                    }
                }
                return View(new ListingSearchModel { ListingsToDisplay = listings.ToList() });
            }
        }
    }
}