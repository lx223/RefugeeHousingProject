using System.Web.Mvc;

namespace RefugeeHousing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Placeholder for About page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Placeholder for contact page.";

            return View();
        }
    }
}