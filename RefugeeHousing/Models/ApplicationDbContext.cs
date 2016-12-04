using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Web.Configuration;
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
        private const string DbConnectionStringEnvironmentVariable = "REFUGEE_HOUSING_DB_CONNECTION_STRING";

        private ApplicationDbContext(DbConnection connection) : base(connection, contextOwnsConnection: true)
        {
        }

        public static ApplicationDbContext Create()
        {
            // Storing the connection string in an environment variable allows us to protect the connection strings
            // on Test and Production
            var connectionString = Environment.GetEnvironmentVariable(DbConnectionStringEnvironmentVariable);

            if (connectionString == null)
            {
                // It's inconvenient to set up extra environment variables in dev, and we use integrated security,
                // so there's nothing to protect - so we just store the connection string in app config
                connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            }

            DbConnection connection = new SqlConnection(connectionString);

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