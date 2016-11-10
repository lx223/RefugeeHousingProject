using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.Services
{
    public class AccountService
    {
        public void SetCookieToUserPreferredLanguage(string userEmail)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindByEmail(userEmail);
            var service = new TranslationService();
            service.SetTranslationCookie(user.PreferredLanguage);
            service.SetTranslationFromCookieIfExists();
        }
    }
}