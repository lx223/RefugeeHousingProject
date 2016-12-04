using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RefugeeHousing.Translations;

namespace RefugeeHousing.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Language PreferredLanguage { get; set; }
    }

    public interface IApplicationDbContext
    {
        IDbSet<Listing> Listings { get; set; }
        IDbSet<Location> Locations { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private ApplicationDbContext(DbConnection connection) : base(connection, contextOwnsConnection: true)
        {
        }

        public static ApplicationDbContext Create()
        {
            DbConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=RefugeeHousing;Integrated Security=True;MultipleActiveResultSets=True;");

            return new ApplicationDbContext(connection);
        }

        public virtual IDbSet<Listing> Listings { get; set; }
        public virtual IDbSet<Location> Locations { get; set; }
    }

    public class DbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            return ApplicationDbContext.Create();
        }
    }
}