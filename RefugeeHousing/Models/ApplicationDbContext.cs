using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RefugeeHousing.Models
{
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