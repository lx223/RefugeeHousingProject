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
                    if (listingSearchModel.MinBedrooms != null)
                    {
                        listings = listings.Where(x => x.NumberOfBedrooms >= (int) listingSearchModel.MinBedrooms);
                    }
                    if (listingSearchModel.MaxPricePerMonth != null)
                    {
                        listings = listings.Where(x => x.Price <= (int)listingSearchModel.MaxPricePerMonth);
                    }
                    if (listingSearchModel.IsFurnished != null)
                    {
                        listings = listings.Where(x => x.Furnished == (bool) listingSearchModel.IsFurnished);
                    }
                }
                return View(new ListingSearchModel { ListingsToDisplay = listings.ToList() });
            }
        }
    }
}