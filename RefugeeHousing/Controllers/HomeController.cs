using System.Threading;
﻿using System.Web.Mvc;
using RefugeeHousing.Translations;

namespace RefugeeHousing.Controllers
{
    [Localization]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var x = Thread.CurrentThread.CurrentCulture;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}