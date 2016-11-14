using System.Web.Mvc;
namespace RefugeeHousing.Translations
{
    public class LocalizationAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            new TranslationService().SetTranslationFromCookieIfExists();
        }
    }
}