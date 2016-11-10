using System.Web.Mvc;
using System.Web.Routing;
using RefugeeHousing.Translations;

namespace RefugeeHousing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new { language = LanguageExtensions.GetDefault().GetCode(), controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
            