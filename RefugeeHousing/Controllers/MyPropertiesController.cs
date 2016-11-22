﻿using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RefugeeHousing.Models;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Controllers
{
    [Authorize]
    public class MyPropertiesController : Controller
    {
        // GET: AddListing
        [HttpGet]
        public ActionResult Add()
        {
            var addListing = new AddListing();
            return View(addListing);
        }

        [HttpPost]
        public ActionResult Add(AddListing addListing)
        {
            using (var db = new ApplicationDbContext())
            {
                var currentUserId = User.Identity.GetUserId();
                var currentUser = db.Users.Single(u => u.Id == currentUserId);

//                listing.Owner = currentUser;
//                db.Listings.Add(listing);

                db.SaveChanges();
            }

            return Redirect("/");
        }
    }
}