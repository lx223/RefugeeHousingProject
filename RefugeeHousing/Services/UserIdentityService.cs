using System.Linq;
using RefugeeHousing.Models;

namespace RefugeeHousing.Services
{
    public class UserIdentityService
    {
        public static ApplicationUser GetUser(ApplicationDbContext db, string userId)
        {
            return db.Users.Single(u => u.Id == userId);
        }
    }
}