using System.Linq;
using RefugeeHousing.Models;

namespace RefugeeHousing.Services
{
    public interface IUserIdentityService
    {
        ApplicationUser GetUser(ApplicationDbContext db, string userId);
    }

    public class UserIdentityService : IUserIdentityService
    {
        public ApplicationUser GetUser(ApplicationDbContext db, string userId)
        {
            return db.Users.Single(u => u.Id == userId);
        }
    }
}