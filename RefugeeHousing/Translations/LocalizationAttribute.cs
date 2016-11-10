using System;
using System.Web.Mvc;
using RefugeeHousing.Models;
using RefugeeHousing.Translations;

namespace RefugeeHousing.ActionFilterAttributes
{
    public class LocalizationAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var languageCode = (string) filterContext.RouteData.Values["languageCode"];
            new TranslationService().SetLanguage(languageCode);
        }
    }
}