using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            if (User.Identity.IsAuthenticated)
            {
                SavePreferenceToDatabase(language);
            }
            service.SetTranslationCookie(language);
            service.SetTranslationFromCookieIfExists();

            var urlReferrer = Request.UrlReferrer;
            if (urlReferrer != null)
            {
                return Redirect(urlReferrer.PathAndQuery);
            }
            return RedirectToAction("Index", "Home");
        }

        private void SavePreferenceToDatabase(Language language)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindById(User.Identity.GetUserId());
            user.PreferredLanguage = language;
            userManager.Update(user);
            userStore.Context.SaveChanges();
        }
    }
}