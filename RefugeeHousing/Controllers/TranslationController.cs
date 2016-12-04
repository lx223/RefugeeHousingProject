using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.Controllers
{
    public class TranslationController : Controller
    {
        private readonly ITranslationService translationService;

        public TranslationController(ITranslationService translationService)
        {
            this.translationService = translationService;
        }

        // GET: Translation
        public ActionResult SetLanguage(Language language)
        {
            if (User.Identity.IsAuthenticated)
            {
                SavePreferenceToDatabase(language);
            }
            translationService.SetTranslationCookie(language);
            translationService.SetTranslationFromCookieIfExists();

            var urlReferrer = Request.UrlReferrer;
            if (urlReferrer != null)
            {
                return Redirect(urlReferrer.PathAndQuery);
            }
            return RedirectToAction("Index", "Home");
        }

        private void SavePreferenceToDatabase(Language language)
        {
            var userStore = new UserStore<ApplicationUser>(ApplicationDbContext.Create());
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            var user = userManager.FindById(User.Identity.GetUserId());
            user.PreferredLanguage = language;
            userManager.Update(user);
            userStore.Context.SaveChanges();
        }
    }
}