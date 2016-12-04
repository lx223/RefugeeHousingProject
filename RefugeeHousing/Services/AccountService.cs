using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.Services
{
    public interface IAccountService
    {
        void SetCookieToUserPreferredLanguage(string userEmail);
    }

    public class AccountService : IAccountService
    {
        public void SetCookieToUserPreferredLanguage(string userEmail)
        {
            using (var db = ApplicationDbContext.Create())
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