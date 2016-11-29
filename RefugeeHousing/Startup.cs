using System.Data.Entity;
using Microsoft.Owin;
using Owin;
using RefugeeHousing.Migrations;
using RefugeeHousing.Models;

[assembly: OwinStartupAttribute(typeof(RefugeeHousing.Startup))]
namespace RefugeeHousing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
