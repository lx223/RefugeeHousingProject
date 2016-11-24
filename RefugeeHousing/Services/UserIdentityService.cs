using System.Linq;
using RefugeeHousing.Models;

namespace RefugeeHousing.Services
{
    public class UserIdentityService
    {
        public static ApplicationUser GetUser(string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Users.Single(u => u.Id == userId);
            }
                
        } 
    }
}