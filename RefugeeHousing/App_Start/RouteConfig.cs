using System.Web.Mvc;
using System.Web.Routing;
using RefugeeHousing.Models;

namespace RefugeeHousing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{languageCode}/{controller}/{action}/{id}",
                defaults: new { languageCode = LanguageExtensions.GetDefault().GetCode(), controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
            