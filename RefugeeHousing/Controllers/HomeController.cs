using System.Web.Mvc;
using NLog;

namespace RefugeeHousing.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            Logger.Info("Homepage requested");

            return View();
        }
    }
}