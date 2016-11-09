using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using RefugeeHousing.Models;

namespace RefugeeHousing.ActionFilterAttributes
{
    public class LocalizationAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var language = (string) filterContext.RouteData.Values["language"] ?? LanguageExtensions.GetDefault().GetCode();
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        }
    }
}