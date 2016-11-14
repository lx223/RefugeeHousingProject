using System.Web.Mvc;
using System.Web.Routing;

namespace RefugeeHousing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //This changes the priority of where routing looks - by default directories are checked before controllers.
            //Setting RouteExistingFiles to true tells ASP.net to always do the routing, even if a file/folder already exists.
            //This was done because of the name clash between the PropertiesController and the Properties folder
            //The name PropertiesController was desired to create /Properties in the url
            routes.RouteExistingFiles = true;

            routes.MapRoute(
                name: "PropertyDetails",
                url: "Properties/{id}",
                defaults: new { controller = "Properties", action = "Details"},
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
            