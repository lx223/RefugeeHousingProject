using System.Web.Mvc;

namespace RefugeeHousing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}