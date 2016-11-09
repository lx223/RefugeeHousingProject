using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RefugeeHousing.Startup))]
namespace RefugeeHousing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
