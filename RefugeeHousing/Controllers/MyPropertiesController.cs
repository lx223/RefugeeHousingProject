using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RefugeeHousing.Models;

namespace RefugeeHousing.Controllers
{
    public class MyPropertiesController : Controller
    {
        // GET: AddListing
        [HttpGet]
        public ActionResult Add()
        {
            var listing = new Listing();
            return View(listing);
        }

        [HttpPost]
        public ActionResult Add(Listing listing)
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