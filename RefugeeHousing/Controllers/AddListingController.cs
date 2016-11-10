using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RefugeeHousing.Models;

namespace RefugeeHousing.Controllers
{
    public class AddListingController : Controller
    {
        // GET: AddListing
        [HttpGet]
        public ActionResult Index()
        {
            var listing = new Listing();
            return View(listing);
        }

        [HttpPost]
        public ActionResult Index(Listing listing)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Listings.Add(listing);
                db.SaveChanges();
            }

            return Redirect("/");
        }
    }
}