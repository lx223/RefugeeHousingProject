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
            using (var db = new ApplicationDbContext())
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = manager.FindByEmail(userEmail);
                var service = new TranslationService();
                service.SetTranslationCookie(user.PreferredLanguage);
                service.SetTranslationFromCookieIfExists();
            }
        }
    }
}