using System.Web.Mvc;
using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.Controllers
{
    public class TranslationController : Controller
    {
        // GET: Translation
        public ActionResult SetLanguage(Language language)
        {
            var service = new TranslationService();
            service.SetTranslationCookie(language);
            service.SetTranslationFromCookieIfExists();

            var urlReferrer = Request.UrlReferrer;
            if (urlReferrer != null)
            {
                return Redirect(urlReferrer.PathAndQuery);
            }
            return RedirectToAction("Index", "Home"); ;
        }
    }
}